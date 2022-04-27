namespace HeatCalculator
{
    public struct Heat
    {
        private readonly double ti;
        private readonly double tf;

        private readonly short sign;
        private double T;
        public const double CVapor = 0.48;
        public const double CWater = 1;
        public const double CIce = 0.5;
        public const double LIceToWater = 80;
        public const double LWaterToVapor = 540;

        public double Q { get => GetQ(); }
        public double M { get; }
        public double Tf { get => sign == 1 ? tf : ti; }
        public double Ti { get => sign == 1 ? ti : tf; }
        public override string ToString()
        {
            return $"{Q} cal";
        }

        public Heat(double m, double ti, double tf)
        {
            if (ti == tf) throw new ArgumentException("as temperaturas não podem ser iguais");
            if (m <= 0) throw new ArgumentException("m não pode ser menor ou igual a zero");
            M = m;
            if (tf - ti < 0)
            {
                //inverte
                this.ti = tf;
                this.tf = ti;
                sign = -1;
            }
            else
            {
                //não inverte
                this.ti = ti;
                this.tf = tf;
                sign = 1;
            }

            T = this.ti;
        }

        private double GetQ()
        {
            double q = 0;
            if (T < 0)//gelo em zero
            {
                q += IceHeat();
            }

            if (T == 0 && tf >= 0)//zero liquido
            {
                q += IceToWaterHeat();
            }

            if (T >= 0 && T < 100)//agua em estado liquido
            {
                q += WaterHeat();
            }
            if (T == 100 && tf >= 100)//vapor a 100
            {
                q += WaterToVaporHeat();
            }
            if (T >= 100 && tf > 100)//vapor a acima de 100
            {
                q += VaporHeat();
            }
            return sign * q;
        }

        private double IceHeat()
        {
            if (tf >= 0)
            {
                var q = M * CIce * (0 - T);
                T = 0;
                return q;
            }
            else
            {
                return M * CIce * (tf - T);
            }
        }
        private double IceToWaterHeat()
        {
            return M * LIceToWater;
        }
        private double WaterHeat()
        {
            if (tf >= 100)
            {
                var q = M * CWater * (100 - T);
                T = 100;
                return q;
            }
            else
            {
                return M * CWater * (tf - T);
            }
        }
        private double WaterToVaporHeat()
        {
            return M * LWaterToVapor;
        }
        private double VaporHeat()
        {
            return M * CVapor * (tf - T);
        }

    }
}