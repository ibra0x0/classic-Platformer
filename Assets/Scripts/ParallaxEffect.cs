using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{

    public Camera cam;
    public Transform followTarget;
    
    // Starting position for the parallax game object
    Vector2 startingPosition;
    
    // Starting z value of the parallax game object
    float startingZ;
    
    // Distance that the camera has moved from the starting position of the parallax object
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    // the further the object from the player, the faster the ParallaxEffect object will move. Drag it's Z value closer to the target to make it move slower
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // when the target moves, move the parallax object the same distance times a multiplier
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;

        // The X/Y position changes based on target travel speed times the parallax factor , but z stays consistent
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
