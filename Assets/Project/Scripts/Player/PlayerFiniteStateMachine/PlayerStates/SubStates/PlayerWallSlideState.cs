﻿public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            core.Movement.SetVelocityY(-playerData.wallSlideVelocity);

            if (grabInput)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
        }
       
    }
}
