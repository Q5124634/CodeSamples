#pragma once

#include <tyga/ActorDelegate.hpp>

class Badger : public tyga::ActorDelegate
{
public:

    /**
     * An actor translated and scaled to the bounding box of the Badger.
     */
    std::shared_ptr<tyga::Actor>
    boundsActor();

private:

    virtual void
    actorDidEnterWorld(std::shared_ptr<tyga::Actor> actor) override;

    virtual void
    actorWillLeaveWorld(std::shared_ptr<tyga::Actor> actor) override;

    virtual void
    actorClockTick(std::shared_ptr<tyga::Actor> actor) override;

    std::shared_ptr<tyga::Actor> physics_actor_;
};
