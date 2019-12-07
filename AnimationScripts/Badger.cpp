#include "Badger.hpp"
#include "MyUtils.hpp"

#include <tyga/Actor.hpp>
#include <tyga/BasicWorldClock.hpp>
#include <tyga/Math.hpp>
#include <tyga/ActorWorld.hpp>
#include <tyga/GraphicsCentre.hpp>


std::shared_ptr<tyga::Actor> Badger::
boundsActor()
{
    return physics_actor_;
}

void Badger::
actorDidEnterWorld(std::shared_ptr<tyga::Actor> actor)
{
    auto world = tyga::ActorWorld::defaultWorld();
    auto graphics = tyga::GraphicsCentre::defaultCentre();

    physics_actor_ = std::make_shared<tyga::Actor>();
    world->addActor(physics_actor_);
}

void Badger::
actorWillLeaveWorld(std::shared_ptr<tyga::Actor> actor)
{
    auto world = tyga::ActorWorld::defaultWorld();

    world->removeActor(physics_actor_);
}

void Badger::
actorClockTick(std::shared_ptr<tyga::Actor> actor)
{
    const float time = tyga::BasicWorldClock::CurrentTime();
	const float delta_time = tyga::BasicWorldClock::CurrentTickInterval();

    auto physics_local_xform = tyga::Matrix4x4(0.75f,     0,     0,     0,
                                                   0,   1.f,     0,     0,
                                                   0,     0,  1.5f,     0,
                                                   0,     1,     0,     1);
    auto physics_xform = physics_local_xform * actor->Transformation();
    physics_actor_->setTransformation(physics_xform);

    const float WHEEL_CIRCUMFERENCE = float(M_PI);
    const float WHEEL_BASE = 2.065f;
    const float MAX_SPEED = 10.f;
    const float MAX_WHEEL_TURN_ANGLE = 0.35f; // radians
}
