// primeira popup do glossario
var popUp1Showing = false;

/*
var animated:GameObject;

var animationFile1:AnimationClip;
var animationFile2:AnimationClip;
var anim1: String;
var anim2: String;
*/
function Start(){
	
	print("Im attached to " + transform.name);
	/* anim1 = animationFile1.name;
	 anim2 = animationFile2.name;
*/
}




function OnMouseEnter(){
renderer.material.color = Color.green;

}


function OnMouseExit(){
renderer.material.color = Color.white;
}

function OnMouseDown(){
renderer.material.color = Color.green;

popUp1Showing = !popUp1Showing;
//animatingGlossary();

			 	//chamada do glossario

}




function OnMouseUp(){
renderer.material.color = Color.white;
}
/*
function animatingGlossary() {
if(popUp1Showing){
	animated.animation.Play(anim1);
} 
else if(!popUp1Showing) {
animated.animation.Play(anim2);
}

}
*/