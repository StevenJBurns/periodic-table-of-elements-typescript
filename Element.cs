using System;
using System.Collections.Generic;

namespace Science.Chemistry
	{
	class AtomicElement
		{
		public Byte AtomicNumber
			{ get; set; }
		
		public String Symbol
			{ get; set; }
		
		public String Name
			{ get; set; }
			
		public Byte Group
			{ get; set; }

		public Byte Period
			{ get; set; }
		
		public Double AtomicWeight
			{ get; set; }

		public Double AtomicRadius
			{ get; set; }
		}
	
	class AtomicElements : List<AtomicElement>
		{
		}
	}
