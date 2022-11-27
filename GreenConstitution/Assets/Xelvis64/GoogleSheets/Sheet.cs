using System;

namespace Xelvis64.GoogleSheets {
	
	[Serializable]
	public struct Sheet {
		
		public SheetProperties properties;
		public GridData[] data;
		
	}
	
}
