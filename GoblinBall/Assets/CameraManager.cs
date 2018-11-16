using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraManager : MonoBehaviour {

	private List<Camera> cams;

	private List<Transform> objs;

	private int n = 4;

	private float[]  positions= {0,0,0,0};

	public float max_margin = 0.005f;
	private float dist = 1.666169f;

	[SerializeField] private float ddd = 0f;

	// Use this for initialization
	void Start () {
		cams = new List<Camera>();
		objs = new List<Transform>();

		GameObject[] cam_objs = GameObject.FindGameObjectsWithTag("MainCamera");
		for (int i = 0; i < cam_objs.Length; i++){
			cams.Add(cam_objs[i].GetComponent<Camera>());
		}
		cams = cams.OrderBy(a=>a.transform.position.x).ToList();
		

		GameObject[] objs_array = GameObject.FindGameObjectsWithTag("camera_follow");
		for (int i = 0; i < objs_array.Length; i++){
			objs.Add(objs_array[i].GetComponent<Transform>());
		}


		n = Mathf.Min(cams.Count, objs.Count);
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < n; i++){
			positions[i] = objs[i].position.x;
		}
		System.Array.Sort(positions);


		int group_n = 1;
		float group_sum = -100000.0f;
		for (int i = 0; i < n; i++){  // TODO: groups don't account for cameras moving left
			if (positions[i] <= group_sum / group_n + dist * (group_n+1)){
				group_n += 1;
				group_sum += positions[i];
			} else {
				if (i > 0){
					align_cameras(group_n, group_sum, i); 
				}
				group_n = 1;
				group_sum = positions[i];
			}
		}
		align_cameras(group_n, group_sum, n);
		
	}

	// aligns the [group_n] cameras preciding camera[end]
	private void align_cameras(int group_n, float group_sum, int end){ 
		for (int j =0; j < group_n; j++){
			int i = end - j - 1;
			positions[i] = group_sum / group_n + dist * (group_n-1) - dist * j * 2;
			float dist_left = 0;
			float dist_right = 0;
			if (i > 0) dist_left = calculate_margin(positions[i], positions[i-1]);
			if (i < n-1) dist_right = calculate_margin(positions[i+1], positions[i]);

			Rect cam_rect = new Rect(0,0,1,1);
			cam_rect.xMin = i * 0.25f  + dist_left;
			cam_rect.xMax = (i+1) * 0.25f - dist_right;
			cams[i].rect = cam_rect;

			cams[i].transform.position = new Vector3(positions[i] + dist * (dist_left - dist_right) * 4, 0,-10);
		}
	}

	private float calculate_margin(float hi, float lo){
		return (float)Mathf.Max((float)System.Math.Tanh((hi - lo - dist*2) / 20)/50f, 0.0f);
	}
}
