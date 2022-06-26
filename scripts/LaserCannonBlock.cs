public class LaserCannonBlock : WeaponBlock
{
	public LaserCannonBlock()
	{
		Logic = new LaserCannonLogic
		{
			RateOfFire = 0.5F,
			Damage = 10.0F,
		};
	}

	class LaserCannonLogic : WeaponLogic
	{
		public bool Shooting { get; set; }
		public float TimeSinceLastShot { get; set; }
		public float RateOfFire { get; set; }
		public float Damage { get; set; }

		public override void StartShooting()
		{
			Shooting = true;
		}

		public override void StopShooting()
		{
			Shooting = false;
		}

		public override void Update(float delta)
		{
			TimeSinceLastShot += delta;
			if (Shooting && TimeSinceLastShot > RateOfFire)
			{
				TimeSinceLastShot = 0.0F;
				Utils.SpawnLaser(Damage, OwningShip.Rotation, OwningShip.ToGlobal(Location), OwningShip);
			}
		}
	}
}
