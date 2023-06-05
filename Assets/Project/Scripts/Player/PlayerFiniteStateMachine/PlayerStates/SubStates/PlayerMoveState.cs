public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.CheckIfShouldFlip(core.Movement.FacingDirection);

        core.Movement.SetVelocityX(playerData.movementVelocity * core.Movement.FacingDirection); 
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
