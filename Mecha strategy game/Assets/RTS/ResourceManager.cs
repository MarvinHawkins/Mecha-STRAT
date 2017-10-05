using UnityEngine;
using System.Collections;

namespace  RTS {
   public static class ResourceManager
    {
        //used to set constant values
        public static float ScrollSpeed { get { return 2; } }
        public static float RotateSpeed { get { return 100; } }
        public static int ScrollWidth { get { return 5; } } //The amount to scroll before th camera moves
        public static float RotateAmount { get { return 10; } }
        public static float MinCameraHeight { get { return 10; } }
        public static float MaxCameraHeight { get { return 40; } }
        private static Vector3 invalidPosition = new Vector3(-99999, -99999, -99999);
        public static Vector3 InvalidPosition { get { return invalidPosition; } }
    }
}
