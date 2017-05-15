#pragma strict

public var slowmotion : boolean;
function Start () {
	
}

function Update () {
 if(slowmotion){
   Time.timeScale = .5;
   Time.fixedDeltaTime = 0.02F * Time.timeScale;
 }else{
   Time.timeScale = 1;
  }
 }
