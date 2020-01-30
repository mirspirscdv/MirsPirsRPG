using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Text;

    class IdleState : IState
{
    public Enemy parent;

    public void Exit()
    {
        
    }

    public void Enter(Enemy parent)
    {
        this.parent = parent;

        this.parent.Reset();
    }

    public void Update()
    {
        if (parent.MyTarget != null)
        {
            parent.ChangeState(new FollowState());
        }
    }
}