using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthenticationTemplate
{
    public class SqlConnectionsAndLogging
    {
        SqlConnection _Connection;
        string _ErrorLogTable;
        string[] _ErrorLogTableColumns;
        string _ApiLogTable;
        string[] _ApiLogTableColumns;
        public SqlConnectionsAndLogging()
        {
            SqlLoggingConfiguration configuration = new SqlLoggingConfiguration();
            SqlConnection connection = new SqlConnection(@"Server = DESKTOP-OJ9JERU\SQLEXPRESS; Database = TestDb; Trusted_Connection = True;");
            _Connection = connection;
            _ErrorLogTable = configuration.ErrorLogTable;
            _ErrorLogTableColumns = configuration.ErrorLogTableColumns;
            _ApiLogTable = configuration.ApiLogTable;
            _ApiLogTableColumns = configuration.ApiLogTableColumns;
        }
        //this method only works as long as the datatypes for each column you want to inser into is a varchar or nvarchar
        public void logErrors(string errorMessage, string[] valuesToInsert)
        {
            if (valuesToInsert.Count() == _ErrorLogTableColumns.Count())
            {
                using (_Connection)
                {
                    _Connection.Open();
                    string InsertIntoQuery = $"Insert Into {_ErrorLogTable} (";
                    string values = "Values(";
                    //adds the list of columns to the insert statement eg "Insert into TableName ('columnOne','columnTwo', and the values to insert eg Values('valueOne','ValueTwo', then puts the two together to create a complete insert into statement
                    for (int i = 0; i < _ErrorLogTableColumns.Count(); i++)
                    {
                        InsertIntoQuery += "'" + $"{_ErrorLogTableColumns[i]}" + "',";
                        values += "'" + $"{valuesToInsert[i]}" + "',";
                    }
                    //formats and joins the strings to make the insert intp statement
                    InsertIntoQuery = InsertIntoQuery.Remove(InsertIntoQuery.Length - 1);
                    InsertIntoQuery += ")";
                    values = values.Remove(values.Length - 1);
                    values += ")";
                    InsertIntoQuery += values;
                    SqlCommand InsertIntoQueryForErrorLog = new SqlCommand(InsertIntoQuery, _Connection);
                }
            }

        }
        public void logApi(DateTime timeApiWasAccessed,string ApiUrl,string userName)
        {
            string logEntry = $"{ApiUrl} was accessed by {userName}, at {timeApiWasAccessed} ";
            string InsertIntoQuery = $"Insert Into {_ErrorLogTable} ('entry') values ('{ApiUrl}')";
        }
    }
}

/*< Parameters >
< InBoxMsgReqd > N </ InBoxMsgReqd >
< Process > Validate </ Process >
< CustomerToUse />
< WarehouseListToUse ></ WarehouseListToUse >
< UseCustomerSalesWarehouse />
< TypeOfOrder > ORD </ TypeOfOrder >
< OrderStatus > 1 </ OrderStatus >
< MinimumDaysToShip ></ MinimumDaysToShip >
< AllowNonStockItems > Y </ AllowNonStockItems >
< AcceptOrdersIfNoCredit > N </ AcceptOrdersIfNoCredit >
< AcceptEarlierShipDate > N </ AcceptEarlierShipDate >
< OperatorToInform > ADMIN </ OperatorToInform >
< CreditFailMessage > No credit available</CreditFailMessage>
<ValidProductClassList></ValidProductClassList>
<ShipFromDefaultBin>N</ShipFromDefaultBin>
   <AllowDuplicateOrderNumbers>N</AllowDuplicateOrderNumbers>
   <CheckForCustomerPoNumbers>N</CheckForCustomerPoNumbers>
   <AllowInvoiceInformationEntry/>
<AlwaysUsePriceEntered/>
<AllowZeroPrice/>
<AllowChangeToZeroPrice/>
<AddStockSalesOrderText>N</AddStockSalesOrderText>
   <AddDangerousGoodsText>N</AddDangerousGoodsText>
   <UseStockDescSupplied/>
<ValidateShippingInstrs/>
<AllocationAction/>
<IgnoreWarnings>N</IgnoreWarnings>
   <AddAttachedServiceCharges/>
<StatusInProcess/>
<StatusInProcessResponse/>
<WarnIfCustomerOnHold>N</WarnIfCustomerOnHold>
   <AcceptKitOptional>N</AcceptKitOptional>
   <AllowBackOrderForPartialHold/>
<AllowBackOrderForSuperseded/>
<OverrideCustomerBackOrder/>
<UseMasterAccountForCustomerPartNo/>
<ApplyLeadTimeCalculation/>
<ApplyParentDiscountToComponents>N</ApplyParentDiscountToComponents>
   <AllowManualOrderNumberToBeUsed>N</AllowManualOrderNumberToBeUsed>
   <ReserveStock/>
<ReserveStockRequestAllocs/>
<AllowBackOrderForNegativeMerchLine>N</AllowBackOrderForNegativeMerchLine>
   </Parameters>
</SalesOrders>";

        string Document = @"
<TransmissionHeader>
<TransmissionReference>00000000000003</TransmissionReference>
<SenderCode/>
<ReceiverCode>HO</ReceiverCode>
<DatePrepared>2006-11-04</DatePrepared>
<TimePrepared>11:55</TimePrepared>
</TransmissionHeader>
<Orders>
<OrderHeader>
<CustomerPoNumber>C1000</CustomerPoNumber>
<OrderActionType>A</OrderActionType>
<NewCustomerPoNumber/>
<Supplier/>
<Customer>000010</Customer>
<OrderDate>2006-11-04</OrderDate>
<InvoiceTerms>0</InvoiceTerms>
<Currency>$</Currency>
<ShippingInstrs>Ship via Hong Kong</ShippingInstrs>
<ShippingInstrsCode>R</ShippingInstrsCode>
<CustomerName>The SYSPRO Outdoors Company</CustomerName>
<ShipAddress1>This is the alternate delivery address 1</ShipAddress1>
<ShipAddress2>This is the alternate delivery address 2</ShipAddress2>
<ShipAddress3>This is the alternate delivery address 3</ShipAddress3>
<ShipAddress3Locality>This is the delivery address 3 location</ShipAddress3Locality>
<ShipAddress4>This is the alternate delivery address 4</ShipAddress4>
<ShipAddress5>This is the alternate delivery address 5</ShipAddress5>
<ShipPostalCode>90210</ShipPostalCode>
<ShipGpsLat>12.123456</ShipGpsLat>
<ShipGpsLong>123.123456</ShipGpsLong>
<LanguageCode/>
<Email>Sender001 @Sender001.com</Email>
<OrderDiscPercent1>2.50</OrderDiscPercent1>
<OrderDiscPercent2>1.50</OrderDiscPercent2>
<OrderDiscPercent3>1.00</OrderDiscPercent3>
<Warehouse>E</Warehouse>
<SpecialInstrs>Handle with care</SpecialInstrs>
<SalesOrder>221124</SalesOrder>
<OrderType>1</OrderType>
<MultiShipCode/>
<ShipAddressPerLine/>
<AlternateReference/>
<Salesperson>100</Salesperson>
<Branch/>
<Area/>
<RequestedShipDate>2006-12-20</RequestedShipDate>
<InvoiceNumberEntered/>
<InvoiceDateEntered/>
<OrderComments/>
<Nationality/>
<DeliveryTerms/>
<TransactionNature/>
<TransportMode/>
<ProcessFlag/>
<TaxExemptNumber/>
<TaxExemptionStatus/>
<GstExemptNumber/>
<GstExemptionStatus/>
<CompanyTaxNumber/>
<ShipAddressPerLineTax> </ShipAddressPerLineTax>
<CancelReasonCode/>
<DocumentFormat/>
<State/>
<CountyZip/>
<City/>
<DeliveryRouteAction>N</DeliveryRouteAction>
<DeliveryRoute/>
<InvoiceWholeOrderOnly/>
<SalesOrderPromoQualifyAction>W</SalesOrderPromoQualifyAction>
<SalesOrderPromoSelectAction>A</SalesOrderPromoSelectAction>
<GlobalTradePromotionCodes>GLACC, GLFREE</GlobalTradePromotionCodes>
<POSSalesOrder/>
<eSignature/>
</OrderHeader>
<OrderDetails>
<StockLine>
<CustomerPoLine>1</CustomerPoLine>
<LineActionType>A</LineActionType>
<LineCancelCode/>
<StockCode>B100</StockCode>
<StockDescription>Bicycle</StockDescription>
<Warehouse>FG</Warehouse>
<CustomersPartNumber>FF334221</CustomersPartNumber>
<OrderQty>5</OrderQty>
<OrderUom>EA</OrderUom>
<Price>400</Price>
<PriceUom>EA</PriceUom>
<PriceCode/>
<AlwaysUsePriceEntered/>
<Units/>
<Pieces/>
<ProductClass/>
<LineDiscPercent1>0.5</LineDiscPercent1>
<LineDiscPercent2>0</LineDiscPercent2>
<LineDiscPercent3>0</LineDiscPercent3>
<AlwaysUseDiscountEntered>N</AlwaysUseDiscountEntered>
<CustRequestDate>2006-12-20</CustRequestDate>
<CommissionCode/>
<LineShipDate/>
<LineDiscValue>0</LineDiscValue>
<LineDiscValFlag/>
<OverrideCalculatedDiscount/>
<UserDefined>USER</UserDefined>
<NonStockedLine/>
<NsProductClass>NSPR</NsProductClass>
<NsUnitCost/>
<UnitMass/>
<UnitVolume/>
<StockTaxCode/>
<StockNotTaxable/>
<StockFstCode/>
<StockNotFstTaxable/>
<AllocationAction/>
<ConfigPrintInv/>
<ConfigPrintDel/>
<ConfigPrintAck/>
<TariffCode/>
<LineMultiShipCode/>
<SupplementaryUnitsFactor/>
<ReserveStock/>
<ReserveStockRequestAllocs/>
<TradePromotionCodes>BIKEACCR, FREE1</TradePromotionCodes>
</StockLine>
<CommentLine>
<CustomerPoLine>2</CustomerPoLine>
<LineActionType>A</LineActionType>
<Comment>Ensure saddle is color coded</Comment>
<AttachedLineNumber>1</AttachedLineNumber>
<CommentType/>
</CommentLine>
<MiscChargeLine>
<CustomerPoLine>3</CustomerPoLine>
<LineActionType>A</LineActionType>
<LineCancelCode/>
<MiscChargeValue>78.56</MiscChargeValue>
<MiscChargeCost>20.00</MiscChargeCost>
<MiscQuantity>3</MiscQuantity>
<MiscProductClass>_OTH</MiscProductClass>
<MiscTaxCode>A</MiscTaxCode>
<MiscNotTaxable/>
<MiscFstCode>B</MiscFstCode>
<MiscNotFstTaxable/>
<MiscDescription>Sundry Items</MiscDescription>
<MiscChargeCode/>
<MiscTariffCode/>
<ConfigPrintInv/>
<ConfigPrintDel/>
<ConfigPrintAck/>
</MiscChargeLine>
<FreightLine>
<CustomerPoLine>4</CustomerPoLine>
<LineActionType>A</LineActionType>
<LineCancelCode/>
<FreightValue>19.00</FreightValue>
<FreightCost>12.00</FreightCost>
<FreightTaxCode>A</FreightTaxCode>
<FreightNotTaxable/>
<FreightFstCode>B</FreightFstCode>
<FreightNotFstTaxable/>
</FreightLine>
</OrderDetails>
</Orders>
</SalesOrders>";
string PrefixDoc = "<SalesOrders xsd:noNamespaceSchemaLocation=+ @\"SORTOIDOC.XSD" + ">\"";
*/
