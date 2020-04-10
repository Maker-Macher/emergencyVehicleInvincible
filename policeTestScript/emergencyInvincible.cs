using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;

[assembly: Rage.Attributes.Plugin("emergencyInvincible", Description = "Makes all vehicles that are labeled as 'Emergency' invincible.", Author = "MakerMacher")]

namespace emergencyInvincible
{
    public static class emergencyInvincible
    {
        public static void Main()
        {
            while (true)
            {
                GameFiber.Yield();

                Vehicle[] veh = World.GetAllVehicles();

                foreach (var i in veh)
                {
                    if (i.Class.ToString() == "Emergency")
                    {
                        if (i.IsDeformationEnabled == true)
                        {
                            GameFiber.StartNew(delegate
                            {
                                i.IsDeformationEnabled = false;
                                i.CanBeDamaged = false;
                                i.IsInvincible = true;
                            });
                        }
                    }
                }
            }
        }

        public static void OnUnload(bool isTerminating)
        {
            Vehicle[] veh = World.GetAllVehicles();

            foreach (var i in veh)
            {
                if (i.Class.ToString() == "Emergency")
                {
                    GameFiber.StartNew(delegate
                    {
                        i.IsDeformationEnabled = true;
                        i.CanBeDamaged = true;
                        i.IsInvincible = false;
                    });
                }
            }
        }
    }
}
