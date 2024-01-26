using System;

public interface IInteractable
{
    public TeamWhoCanGrab WhoCanGrab();
    public void Interact();

    public void ChangeWhoCanGrab(TeamWhoCanGrab whoCanGrab);
}

[Serializable, Flags]
public enum TeamWhoCanGrab
{
    Equipe1 = 1<<0,
    Equipe2 = 1<<1,
}