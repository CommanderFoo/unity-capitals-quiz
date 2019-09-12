using UnityEngine;

namespace net.pixeldepth {
	
	[CreateAssetMenu(fileName = "Country", menuName = "Country")]

	public class Scriptable_Country : ScriptableObject {
			
		[HideInInspector]
		public int index = -2;

		public string country;
		public string capital;

		[TextArea(20,40)]
		public string info;

	}

}