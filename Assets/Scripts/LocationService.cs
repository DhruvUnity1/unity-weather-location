using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using CleverTap.NativeToast;

// Handles GPS location fetching
public class LocationService : MonoBehaviour
{
    public event Action<float, float> OnLocationFetched;

    private const int LOCATION_TIMEOUT = 20;

    public void FetchLocation()
    {
        // Request permission on Android first
#if UNITY_ANDROID && !UNITY_EDITOR
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            NativeToast.Show("Please allow location permission");
            return;
        }
#endif
        StartCoroutine(GetLocation());
    }

    private IEnumerator GetLocation()
    {
        // Check if location services are enabled on device
        if (!Input.location.isEnabledByUser)
        {
            NativeToast.Show("Location services are disabled");
            yield break;
        }

        Input.location.Start();

        // Wait for location to initialize (max 20 seconds)
        int wait = LOCATION_TIMEOUT;
        while (Input.location.status == LocationServiceStatus.Initializing && wait > 0)
        {
            yield return new WaitForSeconds(1);
            wait--;
        }

        if (wait <= 0)
        {
            NativeToast.Show("Location timeout");
            Input.location.Stop();
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            NativeToast.Show("Unable to get location");
            Input.location.Stop();
            yield break;
        }

        // Got location successfully
        var data = Input.location.lastData;
        OnLocationFetched?.Invoke(data.latitude, data.longitude);

        Input.location.Stop();
    }
    #if UNITY_EDITOR
    public void SimulateLocationFetched(float latitude, float longitude)
    {
        OnLocationFetched?.Invoke(latitude, longitude);
    }
#endif
}
