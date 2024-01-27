using System;

public interface IInteractable
{
    public TeamWhoCanGrab WhoCanGrab();
    public IInteractable Interact(PlayerGrab playerGrab);

    public void ChangeWhoCanGrab(TeamWhoCanGrab whoCanGrab);
}

[Serializable, Flags]
public enum TeamWhoCanGrab
{
    Equipe1 = 1<<0,
    Equipe2 = 1<<1,
}