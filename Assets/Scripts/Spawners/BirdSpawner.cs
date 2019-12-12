using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
	public BirdAsset BirdAsset_A;
	public BirdAsset BirdAsset_B;

    public int NumberToSpawn_A, NumberToSpawn_B;

    private List<GameObject> birds = new List<GameObject>();

	void Start()
	{
        SpawnBirds();
	}

    //Spawns birds and moves them to starting positions
    public void SpawnBirds()
    {
            int spawnCount1 = 0, spawnCount2 = 0;

            while (spawnCount1 < NumberToSpawn_A)
            {
                var newBird = Instantiate(BirdAsset_A.BirdPrefab);
                var newBirdMeshes = newBird.GetComponentsInChildren<MeshRenderer>();
                SetBirdColor(newBirdMeshes, BirdAsset_A);
                var birdScript = newBird.AddComponent<Bird>();
                birdScript.SetProperties(BirdAsset_A.flightSpeed, BirdAsset_A.observationTime);

                switch
                (BirdAsset_A.Behaviour)
                {
                    case BirdBehaviour.LandOnTreesWLeaves:
                        birdScript.SetBehaviourLandOnTreesWithLeaves();
                        break;
                    case BirdBehaviour.LandOnAll:
                        birdScript.SetBehaviourLandOnAnyTrees();
                        break;
                    default:
                        birdScript.SetBehaviourLandOnAnyTrees();
                        break;
                }
                var animator = newBird.GetComponent<Animator>();
                animator.runtimeAnimatorController = BirdAsset_A.AnimController;
                animator.enabled = true;
                birds.Add(newBird);
                spawnCount1++;
            }
            while (spawnCount2 < NumberToSpawn_B)
            {
                var newBird = Instantiate(BirdAsset_B.BirdPrefab);
                var newBirdMeshes = newBird.GetComponentsInChildren<MeshRenderer>();
                SetBirdColor(newBirdMeshes, BirdAsset_B);
                var birdScript = newBird.AddComponent<Bird>();
                birdScript.SetProperties(BirdAsset_B.flightSpeed, BirdAsset_B.observationTime);

                switch
                (BirdAsset_B.Behaviour)
                {
                    case BirdBehaviour.LandOnTreesWLeaves:
                        birdScript.SetBehaviourLandOnTreesWithLeaves();
                        break;
                    case BirdBehaviour.LandOnAll:
                        birdScript.SetBehaviourLandOnAnyTrees();
                        break;
                    default:
                        birdScript.SetBehaviourLandOnAnyTrees();
                        break;
                }
                var animator = newBird.GetComponent<Animator>();
                animator.runtimeAnimatorController = BirdAsset_B.AnimController;
                animator.enabled = true;
                birds.Add(newBird);
                spawnCount2++;
            }
            foreach (GameObject bird in birds)
            {
                if (bird.CompareTag("Bird_Any"))
                {
                    bird.GetComponent<Bird>().FlyToSpawn();
                }
                else if (bird.CompareTag("Bird_Leaf"))
                {
                    bird.GetComponent<Bird>().FlyToSpawn();
                }
            }
        }

	private void SetBirdColor(MeshRenderer[] birdRenderers, BirdAsset bAsset)
	{
		foreach (MeshRenderer renderer in birdRenderers)
		{
			renderer.material.SetColor("_Color", bAsset.BirdColor);
		}
	}
}
