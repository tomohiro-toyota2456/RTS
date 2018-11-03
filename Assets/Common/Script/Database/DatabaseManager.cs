using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : UnitySingleton<DatabaseManager>
{ 
	[SerializeField]
	HeadPartDatabase headPartDatabase;
	[SerializeField]
	CorePartDatabase corePartDatabase;
	[SerializeField]
	WeponPartDatabase weponPartDatabase;
	[SerializeField]
	LegPartDatabase legPartDatabase;

	[SerializeField]
	SkillDatabase skillDatabase;
	[SerializeField]
	SkillTreeDatabase skillTreeDatabase;
	[SerializeField]
	SkillDetailParamDatabase skillDetailParamDatabase;

	[SerializeField]
	BulletDatabase bulletDatabase;
	[SerializeField]
	BuffDatabase buffDatabase;


	public HeadPartDatabase GetHeadPartDatabase()
	{
		return headPartDatabase;
	}

	public CorePartDatabase GetCorePartDatabase()
	{
		return corePartDatabase;
	}

	public WeponPartDatabase GetWeponPartDatabase()
	{
		return weponPartDatabase;
	}

	public LegPartDatabase GetLegPartDatabase()
	{
		return legPartDatabase;
	}

	public SkillDatabase GetSkillDatabase()
	{
		return skillDatabase;
	}

	public SkillTreeDatabase GetSkillTreeDatabase()
	{
		return skillTreeDatabase;
	}

	public SkillDetailParamDatabase GetSkillDetailParamDatabase()
	{
		return skillDetailParamDatabase;
	}

	public BulletDatabase GetBulletDatabase()
	{
		return bulletDatabase;
	}

	public BuffDatabase GetBuffDatabase()
	{
		return buffDatabase;
	}

}
