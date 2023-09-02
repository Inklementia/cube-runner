using UnityEngine;

namespace Source.Code
{
    public static class SwipeActions 
    {   
    
    
        public delegate void MoveDelegate(bool[] directions);
        public static MoveDelegate OnMove;
        
        public delegate void ClickDelegate(Vector2 pos);
        public static ClickDelegate OnClick;
    }
}
