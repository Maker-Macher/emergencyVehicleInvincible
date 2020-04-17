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

            string[] copList = {"s_m_y_ranger_01", "s_m_y_sheriff_01", "s_m_y_cop_01", "s_f_y_cop_01", "s_f_y_sheriff_01", "s_m_y_hwaycop_01"};
            while (true)
            {
                GameFiber.Yield();

                Vehicle[] veh = World.GetAllVehicles();

                foreach (var i in veh)
                {
                    if (i.IsValid())
                    {
                        if(i.Class.ToString() == "Emergency")
                        {
                            if (i.HasDriver)
                            {
                                if (i.IsPoliceVehicle)
                                {
                                    Ped driver = i.Driver;

                                    if (copList.Contains(driver.Model.Name.ToLower()) || driver.IsPlayer)
                                    {
                                        GameFiber.StartNew(delegate
                                        {
                                            GameFiber.Sleep(2000);
                                            i.Repair();
                                        });
                                        i.IsDeformationEnabled = false;
                                        i.CanBeDamaged = false;
                                        i.IsInvincible = true;
                                    }
                                }
                            }
                            else
                            {
                                i.IsDeformationEnabled = true;
                                i.CanBeDamaged = true;
                                i.IsInvincible = false;
                            }
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