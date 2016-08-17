using System.Collections;
using UnityEngine;

namespace KangEngine.Util
{
    public sealed class KangGUID
    {
        private static uint _guid = 0;
        public static uint invalidID = 0;

        public static uint Build()
        {
            return ++_guid;
        }
    }

    public sealed class KangHash
    {
        public static uint Build(string str)
        {
            return (uint)UnityEngine.Animator.StringToHash(str);
        }

        public static Hashtable Hash(params object[] args)
        {
            Hashtable hashTable = new Hashtable(args.Length / 2);
            if (args.Length % 2 != 0)
            {
                Debug.LogError("Tween Error: Hash requires an even number of arguments!");
                return null;
            }
            else
            {
                int i = 0;
                while (i < args.Length - 1)
                {
                    hashTable.Add(args[i], args[i + 1]);
                    i += 2;
                }
                return hashTable;
            }
        }
    }

    public static class KangRay
    {
        public static bool RaycastPoint(Ray ray, float distance, int layerMask, out Vector3 point)
        {
            point = Vector3.zero;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, distance, layerMask))
            {
                point = hit.point;
                return true;
            }

            return false;
        }

        public static bool RaycastPoint(Ray ray, float distance, out Vector3 point)
        {
            point = Vector3.zero;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, distance))
            {
                point = hit.point;
                return true;
            }

            return false;
        }
    }

    public static class KangVector
    {
        public static Vector3 MultiplyVectors(Vector3 left, Vector3 right)
        {
            left.x *= right.x;
            left.y *= right.y;
            left.z *= right.z;
            return left;
        }

        public static Vector3 ProjectAxisVector(Vector3 origin, float deg, ProjectAxisType type)
        {
            Vector3 result = Vector3.zero;
            float length = origin.magnitude;
            switch (type)
            {
                case ProjectAxisType.PAT_X:
                    //result.Set(0,length*);
                    break;
            }
            
            return result;
        }
    }

    public enum ProjectAxisType
    {
        PAT_X,
        PAT_Y,
        PAT_Z,
    }
}
