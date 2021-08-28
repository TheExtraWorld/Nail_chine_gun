using Modding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Nail_chine_gun
{
    public class Fury : Mod
    {


        public Fury() : base("Nail-chine Gun") { } 
        public override string GetVersion() => "69.420";
        public bool hasLoaded = false;

        public override void Initialize()
        {
            ModHooks.Instance.SlashHitHook += OnSlashHit;
            ModHooks.Instance.AttackHook += OnAttack;
        }



        public void OnAttack(GlobalEnums.AttackDirection dir)
        {
            if (!hasLoaded)
            {
                On.HeroController.CanDoubleJump -= True;
                On.HeroController.CanDoubleJump += True;
                hasLoaded = true;
            }

            HeroController.instance.ATTACK_COOLDOWN_TIME = 0.01f;
            HeroController.instance.ATTACK_DURATION = 0.01f;
            HeroController.instance.ATTACK_RECOVERY_TIME = 0.01f;
        }

        private bool True(On.HeroController.orig_CanDoubleJump orig, HeroController self)
        {
            return true;
        }



        public void OnSlashHit(Collider2D otherCollider, GameObject gameObject)
        {
            
            try
            {
                HealthManager health = otherCollider.gameObject.GetComponent<HealthManager>();
                float f = ReflectionHelper.GetAttr<HealthManager, float>(health, "evasionByHitRemaining");
                if (Math.Abs(f) > 0.05)
                {
                    ReflectionHelper.SetAttr<HealthManager, float>(health, "evasionByHitRemaining", (Math.Abs(f) / f) * 0.05f);
                }


                Log(f);
            }
            catch (Exception x)
            {
                Log(x.Message);
            }


        }
    }
}
