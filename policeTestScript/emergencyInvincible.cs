using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;

[assembly: Rage.Attributes.Plugin("emergencyInvincible", Description = "Makes all vehicles that are labeled as 'Emergency' as invincible and repairs them if damaged.", Author = "MakerMacher")]

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
                                i.Repair();
                                i.IsDeformationEnabled = false;
                                i.CanBeDamaged = false;
                                i.IsInvincible = true;
                            });
                        }
                        else
                        {
                            i.Repair();
                        }
                    }
                }
            }
        }
    }
}
