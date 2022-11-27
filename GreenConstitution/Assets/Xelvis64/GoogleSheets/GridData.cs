using System;

namespace Xelvis64.GoogleSheets {
	
	[Serializable]
	public struct GridData {
		
		public int startRow;
		public int startColumn;
		public RowData[] rowData;
		
	}
	
}
