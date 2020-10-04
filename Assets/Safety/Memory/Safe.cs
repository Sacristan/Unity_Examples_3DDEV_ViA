using System;

namespace Safe
{
    public static class Functions
    {
        private const int RandomRange = 1000;

        public static int RandomOffset()
        {
            int offset = 0;
            Random rnd = new Random();
            
            do
            {
                offset = rnd.Next(-RandomRange, RandomRange);
            } 
            while (offset == 0);

            return offset;
        }
    }

    public struct Int
    {
        private int offset;
        private int value;

        public Int(int value = 0)
        {
            offset = Functions.RandomOffset();
            this.value = value + offset;
        }

        public int GetValue()
        {
            return value - offset;
        }

        public void Dispose()
        {
            offset = 0;
            value = 0;
        }

        public override string ToString()
        {
            return GetValue().ToString();
        }

        public static Int operator +(Int f1, Int f2)
        {
            return new Int(f1.GetValue() + f2.GetValue());
        }

        public static Int operator -(Int f1, Int f2)
        {
            return new Int(f1.GetValue() - f2.GetValue());
        }
    }

    public struct Float
    {
        private int offset;
        private float value;

        public Float(float value = 0)
        {
            offset = Functions.RandomOffset();
            this.value = value + offset;
        }

        public float GetValue()
        {
            return value - offset;
        }

        public void Dispose()
        {
            offset = 0;
            value = 0;
        }

        public override string ToString()
        {
            return GetValue().ToString();
        }

        public static Float operator +(Float f1, Float f2)
        {
            return new Float(f1.GetValue() + f2.GetValue());
        }

        public static Float operator -(Float f1, Float f2)
        {
            return new Float(f1.GetValue() - f2.GetValue());
        }
    }
}
