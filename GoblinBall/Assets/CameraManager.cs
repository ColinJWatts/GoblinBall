using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	public Camera[] cams;

	public Transform[] objs;

	public int n;

	private float dist = 1;
	private float[]  positions= {0,0,0,0};

	// Use this for initialization
	void Start () {
		dist = cams[0].aspect * cams[0].orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < n; i++){
			positions[i] = objs[i].position.x;
		}
		System.Array.Sort(positions);


		int group_size = 1;
		float group_sum = -100000.0f;
		for (int i = 0; i < n; i++){
			if (positions[i] <= group_sum / group_size + dist * (group_size+1)){
				group_size += 1;
				group_sum += positions[i];
			} else {
				if (i > 0){
					align_cameras(group_size, group_sum, i); 
				}
				group_size = 1;
				group_sum = positions[i];
			}
		}
		align_cameras(group_size, group_sum, n);
		
	}

	// aligns the [group_size] cameras preciding camera[end]
	private void align_cameras(int group_size, float group_sum, int end){ 
		for (int j =0; j < group_size; j++){
			int i = end - j - 1;
			positions[i] = group_sum / group_size + dist * (group_size-1) - dist * j * 2;
			cams[i].transform.position = new Vector3(positions[i], 0,-10);
			cams[i].rect.x = cams[i].rect.x;
		}
	}
}
