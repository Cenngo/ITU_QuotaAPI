﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ITU_QuotaAPI.Models
{
	public interface ILesson
	{
		int CRN { get; set; }
		string Code { get; set; }
		string Day { get; set; }
		string Time { get; set; }
		int Capacity { get; set; }
		int Enroled { get; set; }

		string[] Restrictions { get; set; }

		bool IsEligable ( string major );
	}
}