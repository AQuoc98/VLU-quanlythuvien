export class Constants {
    public static AppSettingsKey: string = 'appsettings';
    public static CredentialsKey: string = 'credentials';

    public static DataTypes: any = {
        Html: 'HTML',
        Checkbox: 'CHECKBOX',
        Number: 'NUMBER',
        UniqueIdentifier: 'UNIQUEIDENTIFIER',
        Select: 'SELECT',
        Email: 'EMAIL',
        Date: 'DATE',
        MultiSelect: 'MULTISELECT',
        Currency: 'CURRENCY',
        Text: 'TEXT',
        MultiText: 'MULTITEXT',
        Link: 'LINK',
        Image: 'IMAGE',
        DateTime: 'DATETIME',
        Time: 'TIME',
        DateRange: 'DATERANGE'
    };

    public static SortExpression: { Asc: string, Desc: string } = {
        Asc: 'ASC',
        Desc: 'DESC'
    };

    public static ResultStatus = {
        Success: 1,
        Error: 2,
        ValidateFail: 3
    };

    public static LoginType = {
        Email: 'dd5fe78a-2ee4-4e8c-a66c-c05025908955'
    };

    public static CurrencyMaskConfig = {
        align: "right",
        allowNegative: true,
        allowZero: true,
        decimal: ",",
        precision: 0,
        prefix: "",
        suffix: "đ",
        thousands: ",",
        nullable: true
    };

    public static EnumCode = {
        DataServiceType: 'DATA_SERVICE_TYPE',
        Region: 'REGION'
    };

    public static DefaultImage: string = 'https://via.placeholder.com/';

    public static EnumValueTypeConfig = {
        Link: '6e2ed2a9-66a0-4535-a4d1-0854c94ff895',
        BlogCategory: 'a82ad7aa-9962-479e-ae2d-47bd213aca43',
        SinglePage: '1238e5b0-60bf-425d-9d06-506733e8dfff',
        ProductType: '6f5639b8-e1ff-48cc-902b-5975be9eaac8'
    };

    public static SchoolModule = {
        NinoBus: 'c3334cee-f82a-49d0-8fd3-3a2985416c60',
        NinoSchool: '69a8ed05-dfd3-4e1a-a9ce-bfc152ace949',
        Both: 'f53601c2-4a3d-47e0-8fc4-5f5926fdff65',
        KCMC: '75e0fe7b-25b1-49ea-bc00-fc2683831e97'
    };

    public static ResourceTypeFee = {
        FeeSchool: "FeeSchool",
        FeeLate: "FeeLate",
        FeeService: "FeeService",
    }
}