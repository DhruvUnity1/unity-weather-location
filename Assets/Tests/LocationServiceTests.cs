using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LocationServiceTests
{
    [Test]
    public void LocationService_Exists()
    {
        GameObject go = new GameObject();
        LocationService locationService = go.AddComponent<LocationService>();
        
        Assert.IsNotNull(locationService);
        
        Object.DestroyImmediate(go);
    }

    [Test]
    public void LocationService_FiresEventOnLocationFetched()
    {
        GameObject go = new GameObject();
        LocationService locationService = go.AddComponent<LocationService>();
        
        bool eventFired = false;
        locationService.OnLocationFetched += (lat, lon) => {
            eventFired = true;
        };
        
        // Manual trigger for testing
        locationService.SimulateLocationFetched(19.07f, 72.87f);
        
        Assert.IsTrue(eventFired);
        
        Object.DestroyImmediate(go);
    }
}