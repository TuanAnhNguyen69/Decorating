using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Lean.Touch;

[System.Serializable]
public class ProductType{
	public Sprite productTypeImage;
	public Button.ButtonClickedEvent showProductOfType;
}
[System.Serializable]
public class Product{
	public Sprite productImage;
	public Button.ButtonClickedEvent showProduct; 
	public GameObject productModel;
	public Vector3 defaultPosition;
	public Quaternion defaultRotation;
	public Vector3 defaultScale ;
}

public class CreateScrollList : MonoBehaviour {
	//private
	List<List<Product>> allProduct;
	Product activingProduct;

	//public
	public GameObject productButton;
	public List<ProductType> productTypeList;
	public List<Product> allBed;
	public List<Product> allChair;
	//public List<Product> allChildren;
	public List<Product> allDrawer;
	public List<Product> allLamp;
	public List<Product> allShelf;
	public List<Product> allSofa;
	public List<Product> allTable;
	public List<Product> allWardrobe;
	public GameObject titlePanel;
	public LeanTouch leanTouch;
	public LeanSelectable leanSelectable;
	public Camera camera;
	public Transform content;
	public Text typeNameText;



	// Use this for initialization
	void Start () {
		//titlePanel = GameObject.Find ("TitlePanel");

		populateTypeList ();
		generateAllProduct ();
		getDefaultState ();
		addModelControl ();
		deactiveAllProductModel ();

	}

	void addModelControl(){
		foreach (var productList in allProduct)
			foreach (var product in productList){
				setRotateControl (product);
				setScaleControl (product);
				setTranslateControl (product);
				product.showProduct.AddListener (delegate{
					activeProductModel (product);
				});
				product.showProduct.AddListener (delegate {
					UDTEventHandler.instance.BuildNewTarget();
				});
		}
	}

	void setRotateControl (Product product){
		LeanRotate leanRotate= product.productModel.AddComponent <LeanRotate>() as LeanRotate;
		leanRotate.IgnoreGuiFingers = true;
		leanRotate.RequiredFingerCount = 2;
		leanRotate.RequiredSelectable = leanSelectable;
		leanRotate.Camera = camera;
		leanRotate.Relative = true;
	}

	void setScaleControl (Product product){
		LeanTranslate leanTranslate = product.productModel.AddComponent<LeanTranslate> () as LeanTranslate;
		leanTranslate.IgnoreGuiFingers = true;
		leanTranslate.RequiredFingerCount = 1;
		leanTranslate.RequiredSelectable = leanSelectable;
		leanTranslate.Camera = camera;
	}
	void setTranslateControl (Product product){
		LeanScale leanScale = product.productModel.AddComponent<LeanScale> () as LeanScale;
		leanScale.IgnoreGuiFingers = true;
		leanScale.RequiredFingerCount = 2;
		leanScale.RequiredSelectable = leanSelectable;
		leanScale.Camera = camera;
	}

	void generateAllProduct(){
		allProduct = new List<List<Product>>();
		allProduct.Add (allBed);
		allProduct.Add (allChair);
		allProduct.Add (allDrawer);
		allProduct.Add (allLamp);
		allProduct.Add (allShelf);
		allProduct.Add (allSofa);
		allProduct.Add (allTable);
		allProduct.Add (allWardrobe);
	}

	void deactiveAllProductModel(){
		foreach (var productList in allProduct)
			foreach (var product in productList)
				product.productModel.SetActive (false);
	}

	void getDefaultState(){
		foreach (var productList in allProduct)
			foreach (var product in productList) {
				product.defaultPosition = product.productModel.transform.position;
				product.defaultRotation = product.productModel.transform.rotation;
				product.defaultScale = product.productModel.transform.localScale;
			}
				
	}

	public void populateTypeList(){
		if (content.childCount >= 0)
			removeAllButton ();
		foreach(var product in productTypeList){
			GameObject newButton = Instantiate (productButton) as GameObject;
			ProductButton Button = newButton.GetComponent<ProductButton> ();
			Button.productImage.sprite = product.productTypeImage;
			Button.button.onClick = product.showProductOfType;
			product.showProductOfType.AddListener (delegate {
				showProductOfType(productTypeList.IndexOf(product)+1);
			});
			newButton.transform.SetParent(content);
		}
		titlePanel.SetActive (false);
		setPading (productTypeList.Count);
	}

	void populateProductList (List<Product> productList){
		foreach(var product in productList){
			GameObject newButton = Instantiate (productButton) as GameObject;
			ProductButton Button = newButton.GetComponent<ProductButton> ();
			Button.productImage.sprite = product.productImage;
			Button.button.onClick = product.showProduct;
			newButton.transform.SetParent(content);
		}
	}
	public void showProductOfType(int ID){
		removeAllButton ();
		List<Product> productList = getProductListById (ID);
		setPading (productList.Count);
		setProductTypeName (ID);
		populateProductList (productList);
		titlePanel.SetActive (true);
	}

	private void setPading(int productCount){
		HorizontalLayoutGroup layout = content.GetComponent<HorizontalLayoutGroup>() as HorizontalLayoutGroup;
		if(productCount < 7){
			layout.padding.left = (2560 - productCount * 300) / 2;
			layout.padding.right = (2560 - productCount * 300) / 2;
		}
		else{
			layout.padding.left = 50;
			layout.padding.right = 50;
		}
		layout.spacing = 100;
	}


	List<Product> getProductListById(int ID){
		switch (ID) {
		case 1:
			return allBed;
			break;
		case 2:
			return allChair;
			break;
		case 3:
			return allDrawer;
			break;
		case 4:
			return allLamp;
			break;
		case 5:
			return allShelf;
			break;
		case 6:
			return allSofa;
			break;
		case 7:
			return allTable;
			break;
		default:
			return allWardrobe;
			break;
		}

	}

	void setProductTypeName(int ID){
		switch (ID) {
		case 1:
			typeNameText.text= "Bed";
			break;
		case 2:
			typeNameText.text= "Chair";
			break;
		case 3:
			typeNameText.text= "Drawer";
			break;
		case 4:
			typeNameText.text= "Lamp";
			break;
		case 5:
			typeNameText.text= "Shelf";
			break;
		case 6:
			typeNameText.text= "Sofa and bench";
			break;
		case 7:
			typeNameText.text= "Table";
			break;
		default:
			typeNameText.text= "Wardrobe";
			break;
		}
	}

	public void removeAllButton(){
		foreach(Transform child in content){
			GameObject.Destroy (child.gameObject);			
		}
	}

	public void activeProductModel(Product product){
		
		if (activingProduct != null) {
			activingProduct.productModel.transform.position = activingProduct.defaultPosition;
			activingProduct.productModel.transform.rotation = activingProduct.defaultRotation;
			activingProduct.productModel.transform.localScale = activingProduct.defaultScale;
			activingProduct.productModel.SetActive (false);
			activingProduct = null;
		}
		activingProduct = product;
		activingProduct.productModel.SetActive (true);
	}
}
