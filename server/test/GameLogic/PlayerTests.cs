using Thuai.Server.GameLogic;

namespace Thuai.Server.Test.GameLogic;

public class PlayerTests
{
    [Fact]
    public void Properties_DefaultValues_ReturnsCorrect()
    {
        // Arrange.
        Player player = new("", 0);

        // Act.
        // No need to act.

        // Assert.
        Assert.Equal("", player.Token);
        Assert.Equal(0, player.ID);
        Assert.Equal(2, player.Speed);
        Assert.Equal(Math.PI / 18, player.TurnSpeed);
        Assert.NotNull(player.PlayerPosition);
        Assert.NotNull(player.PlayerWeapon);
        Assert.NotNull(player.PlayerArmor);
        Assert.Empty(player.PlayerSkills);
    }

    [Fact]
    public void Properties_SetValues_ReturnsCorrect()
    {
        // Arrange.
        Player player = new("", 0);

        // Act.
        player.Token = "token";
        player.ID = 1;
        player.Speed = 1.0;
        player.TurnSpeed = 1.0;
        player.PlayerPosition = new();
        player.PlayerWeapon = new();
        player.PlayerArmor = new();
        player.PlayerSkills = [];

        // Assert.
        Assert.Equal("token", player.Token);
        Assert.Equal(1, player.ID);
        Assert.Equal(1.0, player.Speed);
        Assert.Equal(1.0, player.TurnSpeed);
        Assert.NotNull(player.PlayerPosition);
        Assert.NotNull(player.PlayerWeapon);
        Assert.NotNull(player.PlayerArmor);
        Assert.Empty(player.PlayerSkills);
    }

    [Fact]
    public void Injured_ArmorValueIsEnough_ReturnsCorrect()
    {
        // Arrange.
        Player player = new("", 0);
        player.PlayerArmor.armorValue = 10;
        player.PlayerArmor.health = 10;

        // Act.
        player.Injured(5);

        // Assert.
        Assert.Equal(5, player.PlayerArmor.armorValue);
        Assert.Equal(10, player.PlayerArmor.health);
    }

    [Fact]
    public void Injured_ArmorValueIsNotEnough_ReturnsCorrect()
    {
        // Arrange.
        Player player = new("", 0);
        player.PlayerArmor.armorValue = 5;
        player.PlayerArmor.health = 10;

        // Act.
        player.Injured(10);

        // Assert.
        Assert.Equal(0, player.PlayerArmor.armorValue);
        Assert.Equal(5, player.PlayerArmor.health);
    }

    [Fact]
    public void PlayerMove_InvokeEvent_ReturnsCorrect()
    {
        // Arrange.
        Player player = new("", 0);
        bool eventInvoked = false;
        MoveDirection? direction = null;
        player.PlayerMoveEvent += (sender, args) => {
            eventInvoked = true;
            direction = args.Movedirection;
        };

        // Act.
        player.PlayerMove(MoveDirection.FORTH);

        // Assert.
        Assert.True(eventInvoked);
        Assert.Equal(MoveDirection.FORTH, direction);
    }

    [Fact]
    public void PlayerMove_EventNotSubscribed_ReturnsCorrect()
    {
        // Arrange.
        Player player = new("", 0);

        // Act.
        player.PlayerMove(MoveDirection.FORTH);

        // Assert.
        // No need to assert.
    }

    [Fact]
    public void PlayerTurn_InvokeEvent_ReturnsCorrect()
    {
        // Arrange.
        Player player = new("", 0);
        bool eventInvoked = false;
        TurnDirection? direction = null;
        player.PlayerTurnEvent += (sender, args) => {
            eventInvoked = true;
            direction = args.Turndirection;
        };

        // Act.
        player.PlayerTurn(TurnDirection.CLOCKWISE);

        // Assert.
        Assert.True(eventInvoked);
        Assert.Equal(TurnDirection.CLOCKWISE, direction);
    }

    [Fact]
    public void PlayerTurn_EventNotSubscribed_ReturnsCorrect()
    {
        // Arrange.
        Player player = new("", 0);

        // Act.
        player.PlayerTurn(TurnDirection.CLOCKWISE);

        // Assert.
        // No need to assert.
    }

    [Fact]
    public void PlayerAttack_InvokeEvent_ReturnsCorrect()
    {
        // Arrange.
        Player player = new("", 0);
        bool eventInvoked = false;
        player.PlayerAttackEvent += (sender, args) => {
            eventInvoked = true;
        };

        // Act.
        player.PlayerAttack();

        // Assert.
        Assert.True(eventInvoked);
    }

    [Fact]
    public void PlayerAttack_EventNotSubscribed_ReturnsCorrect()
    {
        // Arrange.
        Player player = new("", 0);

        // Act.
        player.PlayerAttack();

        // Assert.
        // No need to assert.
    }
}