// 1. 씬에 오브젝트 배치
// 2. 오브젝트를 움직이는 컨트롤러 스크립트 작성
// 3. 오브젝트를 생성하는 제네레이터 스크립트 작성
// 4. 오브젝트간의 연동을 관리하는 감독 스크립트 작성

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// 아래 기능을 제공하는 MonoBehaviour의 확장 클래스
/// 1) GameObject.name 과 이름이 같은 필드를 자동으로 로딩
/// 2) Build-in 컴퍼넌트 캐시
/// 3) Component의 모든 이벤트 핸들러를 가상 함수로 선언
/// 주의) 아래 메서드를 오버라이드하면 반드시 base의 해당 메서드를 호출하여야 함
/// Awake
/// OnDisable
/// </summary>
public class MonoBehaviourEx : MonoBehaviour
{
    //protected void LoadFiedlsByName()
    //{
    //    FieldInfo[] fieldInfos = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

    //    foreach (var fieldInfo in fieldInfos)
    //    {
    //        if (fieldInfo.FieldType.IsAssignableFrom(typeof(GameObject)) == false)
    //            continue;

    //        Attribute attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(AutoLoadAttribute));

    //        if (attribute == null)
    //            continue;

    //        var value = GameObject.Find(fieldInfo.Name); // var value = GameObject.Find("needle");
    //        if (value == null)
    //        {
    //            Debug.LogError(string.Format("이름이 {0}인 필드가 없습니다.", fieldInfo.Name));
    //            continue;
    //        }

    //        fieldInfo.SetValue(this, value);
    //    }
    //}

    protected virtual  void Awake() 
    {
        //LoadFiedlsByName();
    }

    #region Components Cache
    private readonly Dictionary<string, Component> _components = new Dictionary<string, Component>();

    public virtual void OnDisable()
    {
        _components.Clear();
    }

    public Transform Transform
    {
        get
        {
            if (_components.ContainsKey("Transform") == false)
                _components.Add("Transform", GetComponent<Transform>());

            return (Transform) _components["Transform"];
        }
    }

    public Image Image
    {
        get
        {
            if (_components.ContainsKey("Image") == false)
                _components.Add("Image", GetComponent<Image>());

            return (Image) _components["Image"];
        }
    }

    public Animation Animation
    {
        get
        {
            if (_components.ContainsKey("Animation") == false)
                _components.Add("Animation", GetComponent<Animation>());

            return (Animation) _components["Animation"];
        }
    }

    public Animator Animator
    {
        get
        {
            if (_components.ContainsKey("Animator") == false)
                _components.Add("Animator", GetComponent<Animator>());

            return (Animator) _components["Animator"];
        }
    }

    public AudioListener AudioListener
    {
        get
        {
            if (_components.ContainsKey("AudioListener") == false)
                _components.Add("AudioListener", GetComponent<AudioListener>());

            return (AudioListener) _components["AudioListener"];
        }
    }

    public AudioSource AudioSource
    {
        get
        {
            if (_components.ContainsKey("AudioSource") == false)
                _components.Add("AudioSource", GetComponent<AudioSource>());

            return (AudioSource) _components["AudioSource"];
        }
    }

    public BoxCollider BoxCollider
    {
        get
        {
            if (_components.ContainsKey("BoxCollider") == false)
                _components.Add("BoxCollider", GetComponent<BoxCollider>());

            return (BoxCollider) _components["BoxCollider"];
        }
    }

    public BoxCollider2D BoxCollider2D
    {
        get
        {
            if (_components.ContainsKey("BoxCollider2D") == false)
                _components.Add("BoxCollider2D", GetComponent<BoxCollider2D>());

            return (BoxCollider2D) _components["BoxCollider2D"];
        }
    }

    public CapsuleCollider CapsuleCollider
    {
        get
        {
            if (_components.ContainsKey("CapsuleCollider") == false)
                _components.Add("CapsuleCollider", GetComponent<CapsuleCollider>());

            return (CapsuleCollider) _components["CapsuleCollider"];
        }
    }

    public CapsuleCollider2D CapsuleCollider2D
    {
        get
        {
            if (_components.ContainsKey("CapsuleCollider2D") == false)
                _components.Add("CapsuleCollider2D", GetComponent<CapsuleCollider2D>());

            return (CapsuleCollider2D) _components["CapsuleCollider2D"];
        }
    }

    public CircleCollider2D CircleCollider2D
    {
        get
        {
            if (_components.ContainsKey("CircleCollider2D") == false)
                _components.Add("CircleCollider2D", GetComponent<CircleCollider2D>());

            return (CircleCollider2D) _components["CircleCollider2D"];
        }
    }

    public Collider Collider
    {
        get
        {
            if (_components.ContainsKey("Collider") == false)
                _components.Add("Collider", GetComponent<Collider>());

            return (Collider) _components["Collider"];
        }
    }

    public Collider2D Collider2D
    {
        get
        {
            if (_components.ContainsKey("Collider2D") == false)
                _components.Add("Collider2D", GetComponent<Collider2D>());

            return (Collider2D) _components["Collider2D"];
        }
    }

    public CompositeCollider2D CompositeCollider2D
    {
        get
        {
            if (_components.ContainsKey("CompositeCollider2D") == false)
                _components.Add("CompositeCollider2D", GetComponent<CompositeCollider2D>());

            return (CompositeCollider2D) _components["CompositeCollider2D"];
        }
    }

    public ConstantForce ConstantForce
    {
        get
        {
            if (_components.ContainsKey("ConstantForce") == false)
                _components.Add("ConstantForce", GetComponent<ConstantForce>());

            return (ConstantForce) _components["ConstantForce"];
        }
    }

    public ConstantForce2D ConstantForce2D
    {
        get
        {
            if (_components.ContainsKey("ConstantForce2D") == false)
                _components.Add("ConstantForce2D", GetComponent<ConstantForce2D>());

            return (ConstantForce2D) _components["ConstantForce2D"];
        }
    }

    public DistanceJoint2D DistanceJoint2D
    {
        get
        {
            if (_components.ContainsKey("DistanceJoint2D") == false)
                _components.Add("DistanceJoint2D", GetComponent<DistanceJoint2D>());

            return (DistanceJoint2D) _components["DistanceJoint2D"];
        }
    }

    public EdgeCollider2D EdgeCollider2D
    {
        get
        {
            if (_components.ContainsKey("EdgeCollider2D") == false)
                _components.Add("EdgeCollider2D", GetComponent<EdgeCollider2D>());

            return (EdgeCollider2D) _components["EdgeCollider2D"];
        }
    }

    public Effector2D Effector2D
    {
        get
        {
            if (_components.ContainsKey("Effector2D") == false)
                _components.Add("Effector2D", GetComponent<Effector2D>());

            return (Effector2D) _components["Effector2D"];
        }
    }

    public FixedJoint FixedJoint
    {
        get
        {
            if (_components.ContainsKey("FixedJoint") == false)
                _components.Add("FixedJoint", GetComponent<FixedJoint>());

            return (FixedJoint) _components["FixedJoint"];
        }
    }

    public FixedJoint2D FixedJoint2D
    {
        get
        {
            if (_components.ContainsKey("FixedJoint2D") == false)
                _components.Add("FixedJoint2D", GetComponent<FixedJoint2D>());

            return (FixedJoint2D) _components["FixedJoint2D"];
        }
    }

    public FrictionJoint2D FrictionJoint2D
    {
        get
        {
            if (_components.ContainsKey("FrictionJoint2D") == false)
                _components.Add("FrictionJoint2D", GetComponent<FrictionJoint2D>());

            return (FrictionJoint2D) _components["FrictionJoint2D"];
        }
    }

    public Grid Grid
    {
        get
        {
            if (_components.ContainsKey("Grid") == false)
                _components.Add("Grid", GetComponent<Grid>());

            return (Grid) _components["Grid"];
        }
    }

    public GridLayout GridLayout
    {
        get
        {
            if (_components.ContainsKey("GridLayout") == false)
                _components.Add("GridLayout", GetComponent<GridLayout>());

            return (GridLayout) _components["GridLayout"];
        }
    }

    public GUIElement GUIElement
    {
        get
        {
            if (_components.ContainsKey("GUIElement") == false)
                _components.Add("GUIElement", GetComponent<GUIElement>());

            return (GUIElement) _components["GUIElement"];
        }
    }

    public Text Text
    {
        get
        {
            if (_components.ContainsKey("Text") == false)
                _components.Add("Text", GetComponent<Text>());

            return (Text) _components["Text"];
        }
    }

    public MeshCollider MeshCollider
    {
        get
        {
            if (_components.ContainsKey("MeshCollider") == false)
                _components.Add("MeshCollider", GetComponent<MeshCollider>());

            return (MeshCollider) _components["MeshCollider"];
        }
    }

    public ParticleSystem ParticleSystem
    {
        get
        {
            if (_components.ContainsKey("ParticleSystem") == false)
                _components.Add("ParticleSystem", GetComponent<ParticleSystem>());

            return (ParticleSystem) _components["ParticleSystem"];
        }
    }

    public PolygonCollider2D PolygonCollider2D
    {
        get
        {
            if (_components.ContainsKey("PolygonCollider2D") == false)
                _components.Add("PolygonCollider2D", GetComponent<PolygonCollider2D>());

            return (PolygonCollider2D) _components["PolygonCollider2D"];
        }
    }

    public RectTransform RectTransform
    {
        get
        {
            if (_components.ContainsKey("RectTransform") == false)
                _components.Add("RectTransform", GetComponent<RectTransform>());

            return (RectTransform) _components["RectTransform"];
        }
    }

    public Rigidbody Rigidbody
    {
        get
        {
            if (_components.ContainsKey("Rigidbody") == false)
                _components.Add("Rigidbody", GetComponent<Rigidbody>());

            return (Rigidbody) _components["Rigidbody"];
        }
    }

    public Rigidbody2D Rigidbody2D
    {
        get
        {
            if (_components.ContainsKey("Rigidbody2D") == false)
                _components.Add("Rigidbody2D", GetComponent<Rigidbody2D>());

            return (Rigidbody2D) _components["Rigidbody2D"];
        }
    }

    public SphereCollider SphereCollider
    {
        get
        {
            if (_components.ContainsKey("SphereCollider") == false)
                _components.Add("SphereCollider", GetComponent<SphereCollider>());

            return (SphereCollider) _components["SphereCollider"];
        }
    }

    public TerrainCollider TerrainCollider
    {
        get
        {
            if (_components.ContainsKey("TerrainCollider") == false)
                _components.Add("TerrainCollider", GetComponent<TerrainCollider>());

            return (TerrainCollider) _components["TerrainCollider"];
        }
    }

    public TilemapCollider2D TilemapCollider2D
    {
        get
        {
            if (_components.ContainsKey("TilemapCollider2D") == false)
                _components.Add("TilemapCollider2D", GetComponent<TilemapCollider2D>());

            return (TilemapCollider2D) _components["TilemapCollider2D"];
        }
    }

    public VideoPlayer VideoPlayer
    {
        get
        {
            if (_components.ContainsKey("VideoPlayer") == false)
                _components.Add("VideoPlayer", GetComponent<VideoPlayer>());

            return (VideoPlayer) _components["VideoPlayer"];
        }
    }

    public WheelCollider WheelCollider
    {
        get
        {
            if (_components.ContainsKey("WheelCollider") == false)
                _components.Add("WheelCollider", GetComponent<WheelCollider>());

            return (WheelCollider) _components["WheelCollider"];
        }
    }

    #endregion

    #region Overriden EventHandlers
    protected virtual void FixedUpdate()
    {
    }

    protected virtual void LateUpdate()
    {
    }

    protected virtual void OnAnimatorIK(int layerIndex)
    {
    }

    protected virtual void OnAnimatorMove()
    {
    }

    protected virtual void OnApplicationFocus(bool focusStatus)
    {
    }

    protected virtual void OnApplicationPause(bool pauseStatus)
    {
    }

    protected virtual void OnApplicationQuit()
    {
    }

    protected virtual void OnAudioFilterRead(float[] data, int channels)
    {
    }

    protected virtual void OnBecameInvisible()
    {
    }

    protected virtual void OnBecameVisible()
    {
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
    }

    protected virtual void OnCollisionEnter2D(Collision2D coll)
    {
    }

    protected virtual void OnCollisionExit(Collision collisionInfo)
    {
    }

    protected virtual void OnCollisionExit2D(Collision2D coll)
    {
    }

    protected virtual void OnCollisionStay(Collision collisionInfo)
    {
    }

    protected virtual void OnCollisionStay2D(Collision2D coll)
    {
    }

    protected virtual void OnConnectedToServer()
    {
    }

    protected virtual void OnControllerColliderHit(ControllerColliderHit hit)
    {
    }

    protected virtual void OnDestroy()
    {
    }

    protected virtual void OnDrawGizmos()
    {
    }

    protected virtual void OnDrawGizmosSelected()
    {
    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnGUI()
    {
    }

    protected virtual void OnJointBreak(float breakForce)
    {
    }

    protected virtual void OnJointBreak2D(Joint2D brokenJoint)
    {
    }

    protected virtual void OnMouseDown()
    {
    }

    protected virtual void OnMouseDrag()
    {
    }

    protected virtual void OnMouseEnter()
    {
    }

    protected virtual void OnMouseOver()
    {
    }

    protected virtual void OnMouseUp()
    {
    }

    protected virtual void OnMouseUpAsButton()
    {
    }

    protected virtual void OnParticleCollision(GameObject other)
    {
    }

    protected virtual void OnPostRender()
    {
    }

    protected virtual void OnPreCull()
    {
    }

    protected virtual void OnPreRender()
    {
    }

    protected virtual void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
    }

    protected virtual void OnRenderObject()
    {
    }

    protected virtual void OnServerInitialized()
    {
    }

    protected virtual void OnTransformChildrenChanged()
    {
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
    }

    protected virtual void OnTriggerExit(Collider other)
    {
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
    }

    protected virtual void OnTriggerStay(Collider other)
    {
    }

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
    }

    protected virtual void OnValidate()
    {
    }

    protected virtual void OnWillRenderObject()
    {
    }

    protected virtual void Reset()
    {
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }
    #endregion
}
