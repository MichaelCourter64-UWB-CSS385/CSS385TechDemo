using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _3rdPersonCamera : MonoBehaviour
{
    // Serial vert and hori sensitivity
    [SerializeField] float horizontalSensitivity = 0.01f;
    [SerializeField] float verticalSensitivity = 0.01f;
    // Upper vertical angle degree limit.
    [SerializeField] float upperVerticalAngleLimit = 45;
    // Serial magnitude for distance between camera and player
    [SerializeField] float distanceBetween = 5;
    // Lower vertical angle degree limit.
    [SerializeField] float lowerVerticalAngleLimit = -45;
    // The player's character.
    [SerializeField] GameObject playerFollowPoint;
    public GameObject PlayerFollowPoint { set { playerFollowPoint = value; } }

    // Keeps track of the direction the camera should be in, in regards to the
    //     player follow point.
    Vector3 direction = new Vector3(0, 0, 0);

    // Updates the position and rotation of the camera.
    //
    void LateUpdate()
    {
        const short RATIO_ANALOG_DEGREES = 360;

        // The position the camera should be at in the local space of the player follow point.
        Vector3 completePoint = new Vector3();
        // The -1 to 1 range rotation the camera should have.
        Vector3 completeAngle = new Vector3();

        // Adds the look input to the rotation of the camera.
        direction += new Vector3(-Input.GetAxis("Mouse Y") * verticalSensitivity,
            Input.GetAxis("Mouse X") * horizontalSensitivity, 0);

        // If the absolute value of the x-axis rotation is greater than or equal to 1
        if (Mathf.Abs(direction.x) >= 1)
        {
            // Set the ones place value of the value to 0 to loop the rotation.
            direction.x = direction.x % 1;
        }
        // If the absolute value of the y-axis rotation is greater than or equal to 1
        if (Mathf.Abs(direction.y) >= 1)
        {
            // Set the ones place value of the value to 0 to loop the rotation.
            direction.y = direction.y % 1;
        }
        
        // Limits the rotation in the x-axis to the given lower and upper limits.
        direction.x = Mathf.Clamp(direction.x, lowerVerticalAngleLimit / RATIO_ANALOG_DEGREES, upperVerticalAngleLimit / RATIO_ANALOG_DEGREES);
        
        // Calculates the position the camera should be at based on the
        //     direction being turned into a position that is multiplied by the
        //     given magnitude.
        completePoint = Quaternion.Euler(direction * RATIO_ANALOG_DEGREES) * Vector3.forward * distanceBetween;
        // Positions the Gameobject (camera) at the complete point in the local
        //     space of the player follow point.
        transform.position = playerFollowPoint.transform.TransformPoint(completePoint);

        // Copies the direction the camera should be in.
        completeAngle = direction;

        // If the complete angle is greater than or equal to 0, then:
        if (completeAngle.y >= 0)
        {
            // Rotate it on the y-axis to look at the player follow point.
            completeAngle.y += -0.5f;
        }
        else
        {
            // Rotate it on the y-axis to look at the player follow point.
            completeAngle.y += 0.5f;
        }

        // Reverses the rotation on the x-axis so that the camera looks at the
        //     player follow point as it's rotated vertically.
        completeAngle.x *= -1;

        // Sets the rotation of the camera to the complete angle in degrees.
        transform.eulerAngles = completeAngle * RATIO_ANALOG_DEGREES;
    }
}
