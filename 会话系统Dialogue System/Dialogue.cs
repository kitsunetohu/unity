using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

	public string name;


	[TextArea(3, 10)]
	public string[] sentences;
	//可以在sentences中插入符号分割信息，处理字符转时进行解析。比图【1，说了一句话】1可以表示人物的立绘

}
