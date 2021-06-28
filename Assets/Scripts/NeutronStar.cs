﻿using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class NeutronStar : Exotic
{
    public float rotationSpeed = 7000000.0f;

    Behaviour halo;

    protected override void OnEnable()
    {
        base.OnEnable();

        this.rb.isKinematic = true;
    }

    public static NeutronStarVFX Create(Transform parent, Vector3 position, float diameter, float mass, float rotationSpeed)
    {
        GameObject obj = new GameObject("Neutron Star");
        obj.transform.parent = parent;
        obj.transform.position = position;

        GameObject starObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        starObj.transform.parent = obj.transform;
        starObj.transform.localPosition = Vector3.zero;

        NeutronStar neutronStar = starObj.AddComponent<NeutronStar>();
        neutronStar.SetName("Neutron Star");
        neutronStar.SetDiameter(diameter);
        neutronStar.SetMass(mass);
        neutronStar.SetMat(Singleton.Instance.neutronStarMat);
        neutronStar.temperature = Random.Range(1000000.0f, 1000000000000.0f);
        neutronStar.halo = neutronStar.CreateHalo(neutronStar.diameter + 1.0f, new Color(0.666f, 1.0f, 1.0f));
        neutronStar.rotationSpeed = rotationSpeed;

        GameObject vfxObj = Instantiate(Singleton.Instance.neutronStarVFX, obj.transform.position, Quaternion.identity, obj.transform);
        NeutronStarVFX vfx = vfxObj.GetComponent<NeutronStarVFX>();

        vfx.SetBeams(diameter * 0.1f);

        return null;
    }

    private void Update()
    {
        if (Application.isPlaying)
        {
            this.transform.Rotate(Vector3.up * this.rotationSpeed * Time.deltaTime);
        }
    }
}