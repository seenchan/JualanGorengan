using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools {

    protected GameObject ThisObject = null;

    public Tools(GameObject t_object)
    {
        ThisObject = t_object;
    }

    public virtual FryingStatus Status() { return FryingStatus.empty; }
    public virtual void Cook() { }
    public virtual void Addlevel() { }
    public virtual void Use(Gorengan gorengan) { }
    public virtual Gorengan CheckGorengan() { return null; }
    public virtual void AddGorengan(Gorengan gorengan) { }
    public virtual void OnObjectClick() { }
    public virtual int GetTotal() { return 0; }
    public virtual void MinGorengan(int total) { }
}
