export class Product { 
    public id:any;
    public name:string;

    public fromJSON(data: { name:string }): Product {
        let product = new Product();
        product.name = data.name;
        return product;
    }
}
