#include "MyPhysicsCentre.hpp"

#include "MyUtils.hpp"
#include "Badger.hpp"
#include <tyga/InputStateProtocol.hpp>
#include <tyga/BasicWorldClock.hpp>
#include <tyga/Log.hpp>


tyga::Vector3 PhysicsObject::
position() const
{
	auto actor = this->Actor();
	if (actor != nullptr) {
		const auto& xform = actor->Transformation();
		return tyga::Vector3(xform._30, xform._31, xform._32);
	}
	return tyga::Vector3();
}

tyga::Vector3 PhysicsPlane::
normal() const
{
	auto actor = this->Actor();
	if (actor != nullptr) {
		const auto& xform = actor->Transformation();
		return tyga::Vector3(xform._20, xform._21, xform._22);
	}
	return tyga::Vector3();
}

tyga::Vector3 PhysicsBox::
U() const
{
	auto actor = this->Actor();
	if (actor != nullptr) {
		const auto& xform = actor->Transformation();
		return tyga::Vector3(xform._00, xform._01, xform._02);
	}
	return tyga::Vector3();
}

tyga::Vector3 PhysicsBox::
V() const
{
	auto actor = this->Actor();
	if (actor != nullptr) {
		const auto& xform = actor->Transformation();
		return tyga::Vector3(xform._10, xform._11, xform._12);
	}
	return tyga::Vector3();
}

tyga::Vector3 PhysicsBox::
W() const
{
	auto actor = this->Actor();
	if (actor != nullptr) {
		const auto& xform = actor->Transformation();
		return tyga::Vector3(xform._20, xform._21, xform._22);
	}
	return tyga::Vector3();
}

std::shared_ptr<MyPhysicsCentre> MyPhysicsCentre::default_centre_;

std::shared_ptr<MyPhysicsCentre> MyPhysicsCentre::
defaultCentre()
{
	if (default_centre_ == nullptr) {
		default_centre_ = std::make_shared<MyPhysicsCentre>();
	}
	return default_centre_;
}

template<typename T> static void
_prune(std::list<T>& list)
{
	for (auto it = list.begin(); it != list.end(); ) {
		if (it->expired()) {
			it = list.erase(it);
		}
		else {
			it++;
		}
	}
}

void MyPhysicsCentre::
pruneDeadObjects()
{
	_prune(planes_);
	_prune(spheres_);
	_prune(boxes_);
}

std::shared_ptr<PhysicsPlane> MyPhysicsCentre::
newPlane()
{
	auto new_plane = std::make_shared<PhysicsPlane>();
	planes_.push_back(new_plane);
	return new_plane;
}

std::shared_ptr<PhysicsSphere> MyPhysicsCentre::
newSphere()
{
	auto new_sphere = std::make_shared<PhysicsSphere>();
	spheres_.push_back(new_sphere);
	return new_sphere;
}

std::shared_ptr<PhysicsBox> MyPhysicsCentre::
newBox()
{
	auto new_box = std::make_shared<PhysicsBox>();
	boxes_.push_back(new_box);
	return new_box;
}
void MyPhysicsCentre::
runloopWillBegin()

{
	// this stes delta time to the current clock interval
	const float delta_time = tyga::BasicWorldClock::CurrentTickInterval();

	for (auto Col_f = spheres_.begin(); Col_f != spheres_.end(); Col_f++)
	{
		// only continue if a strong reference is available
		if (Col_f->expired()) continue;
		auto sphere = Col_f->lock();

		// this sets the transformation of the sphere when it hits the ground and reverses the velocity when it impacts with the floor
		const auto& pos = sphere->position();
		if (pos.y < sphere->radius) {

			tyga::Vector3 reflectedvelovity;
			
			//this reverses the velocity
			sphere->velocity.y = sphere->velocity.y - (2 * sphere->velocity.y);
			
			auto actor = sphere->Actor();
			if (actor != nullptr) {
				auto xform = actor->Transformation();
				xform._31 = sphere->radius;
				actor->setTransformation(xform);
			}
		}
		//this loops untill oCol_f doesnt = spheres_.end so it does whats in the loop for all current spheres 
		for (auto oCol_f = spheres_.begin(); oCol_f != spheres_.end(); oCol_f++)
		{

			const auto& colSphere = oCol_f->lock();


			//this sets the true positions of the current sphere
			double xPos = pos.x - colSphere->position().x;
			double yPos = pos.y - colSphere->position().y;
			double zPos = pos.z - colSphere->position().z;

			//creates variables to work our the radius
			double xPosSq = xPos * xPos;
			double yPosSq = yPos * yPos;
			double zPosSq = zPos * zPos;
			 //calculates the diffrence in the radius of the spheres
			double radiusCalc = sphere->radius + sphere->radius;
			double radiusSqu = radiusCalc*radiusCalc;

			if (xPosSq + yPosSq + zPosSq <= radiusSqu)
			{
				//collision has occoured
				//swap velocities for collision physics
				tyga::Vector3 tempvelocity = sphere->velocity;
				//swapping the velocitites of the two  spheres that collided
				sphere->velocity = colSphere->velocity;
				colSphere->velocity = tempvelocity;
			}
		}
	}
}

void MyPhysicsCentre::
runloopExecuteTask()
{
	const float time = tyga::BasicWorldClock::CurrentTime();
	const float delta_time = tyga::BasicWorldClock::CurrentTickInterval();

	for (auto ptr : spheres_) {
		// only continue if a strong reference is available
		if (ptr.expired()) continue;
		auto model = ptr.lock();

		// only continue if the model is attached to an actor
		auto actor = model->Actor();
		if (actor == nullptr) continue;

		// TODO: perform the physics (dynamics) update
		float grav = (-9.81);
		/*
		*A vector that applies the acceleration due to gravity in the Y direction.
		*/
		tyga::Vector3 gravity{ 0, grav, 0 };
		tyga::Vector3 position = model->position();
		/*
		*Newtons 2nd law (f=ma) re-arranged and used to get acceleration (a=f/m)
		*/
		tyga::Vector3 acceleration = (model->force + gravity) / model->mass;
		tyga::Vector3 v = TomBarnabyPass::euler(model->velocity, delta_time, acceleration );
		model->velocity = v;
		tyga::Vector3 new_position = TomBarnabyPass::euler(position, delta_time, model->velocity);

		// TODO: update the actor's transformation
		actor->setTransformation(TomBarnabyPass::translate(new_position.x, new_position.y, new_position.z));

		// reset the force
		model->force = tyga::Vector3(0, 0, 0);
	}
}

void MyPhysicsCentre::
runloopDidEnd()
{
	pruneDeadObjects();
}
