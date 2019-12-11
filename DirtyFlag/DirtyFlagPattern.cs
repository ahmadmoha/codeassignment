
using UnityEngine;
using System.Collections;
using System;

namespace DirtyFlagPatternExample
{
    public class DirtyFlagPatternExample : MonoBehaviour
    {
        GraphNode graphNode = new GraphNode(new MeshEX());
        TransformEX parentWorldTransform = new TransformEX();
        void Start()
        {
            for (int i = 0; i < graphNode.NumChildren; i++)
            {
                graphNode.Children[i] = new GraphNode(new MeshEX());
            }
            graphNode.render(TransformEX.origin, true);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                //Location Change,Dirty to press
                TransformEX newLocalTransform = new TransformEX();
                newLocalTransform.Position = new Vector3(2, 2, 2);
                graphNode.setTransform(newLocalTransform);
                graphNode.render(parentWorldTransform, true);
            }
        }
    }


    class TransformEX
    {
        private Vector3 position = new Vector3(1, 1, 1);
        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public static TransformEX origin = new TransformEX();
        public TransformEX combine(TransformEX other)
        {

            TransformEX trans = new TransformEX();
            if (other != null)
            {
                trans.Position = Position + other.Position;
            }
            return trans;
        }

    };

    class GraphNode
    {
        // dirty flag
        private bool dirty_;

        private MeshEX mesh_;
        private TransformEX local_;
        private TransformEX world_ = new TransformEX();
        const int MAX_CHILDREN = 100;

        private GraphNode[] children_ = new GraphNode[MAX_CHILDREN];
        public GraphNode[] Children
        {
            get { return children_; }
            set { children_ = value; }
        }

        private int numChildren_ = 88;
        public int NumChildren
        {
            get { return numChildren_; }
            set { numChildren_ = value; }
        }

        public GraphNode(MeshEX mesh)
        {
            mesh_ = mesh;
            local_ = TransformEX.origin;
            dirty_ = true;

        }

        public void setTransform(TransformEX local)
        {
            local_ = local;
            dirty_ = true;
        }

        public void render(TransformEX parentWorld, bool dirty)
        {
            //if object from parent be dirty than it will be true
            dirty |= dirty_;

            //if not changed than （dirty=false），jump it from combine
            if (dirty)
            {
                Debug.Log("this node is dirty,combine it!");
                world_ = local_.combine(parentWorld);
                dirty_ = false;
            }

            //renderMesh
            if (mesh_ != null)
            {
                renderMesh(mesh_, world_);
            }

            for (int i = 0; i < numChildren_; i++)
            {
                if (children_[i] != null)
                {
                    children_[i].render(world_, dirty);
                }

            }
        }

        private void renderMesh(MeshEX mesh_, TransformEX world_)
        {
            Debug.Log("renderMesh!");
        }
    }

}

