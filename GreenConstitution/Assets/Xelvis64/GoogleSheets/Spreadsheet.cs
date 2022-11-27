using System;

namespace Xelvis64.GoogleSheets {
	
	[Serializable]
	public struct Spreadsheet {
		
		public string spreadsheetId;
		public Sheet[] sheets;
		public string spreadsheetUrl;
		
	}
	
}
