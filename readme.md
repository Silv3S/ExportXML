# Export of report to XML
This .NET Core 3.1 console application provides:
- deserialization of ProductionResults.xml,
- serialization of car objects to XML file
- converting XML file to CSV via XSLT.

## Table of contents
1. Classes
2. Serialization
3. Conversion to CSV using XSLT
4. Example
5. Unit tests

## Classes
![ProductionReport and subclasses](Images/class.png)

## Serialization
Serialization and deserialization is done by *System.Xml.Serialization* library. Fields in classes are decorated with attributes identifying their position in XML file.
- Serialization is implemented as void method that takes list of cars and output file name as argument,
- Deserialization is implemented as method that takes path to file as argument and returns ProductionReport object.

## Conversion to CSV using XSLT
List of cars is converted to CSV format with header in first line. Features of each car are ommited.
## Example

![Application flowchart](Images/diag.png)

<details><summary>ProductionResults.xml</summary>
<p>

```xml
<ProductionReport Manufacturer="Opel" Date="2015-02-18T12:32:02.8036669+01:00">
    <Factories>
        <Factory Name="Monachium Fab">
            <ProducedCars>
                <Car VIN="O002ABC002">
                    <ProductionYear>2007</ProductionYear>
                    <Model>Vectra</Model>
                    <Features>
                        <Feature Code="RXE">Reflektory xenonowe</Feature>
                        <Feature Code="SZW">Sportowe zawieszenie</Feature>
                        <Feature Code="SZD">Szyberdach</Feature>
                    </Features>
                </Car>
                <Car VIN="O004ABC004">
                    <ProductionYear>2005</ProductionYear>
                    <Model>Astra</Model>
                    <Features>
                        <Feature Code="RXE">Reflektory xenonowe</Feature>
                    </Features>
                </Car>
            </ProducedCars>
        </Factory>
    </Factories>
</ProductionReport>
```

</p>
</details>


<details><summary>Cars.xml</summary>
<p>

```xml
<?xml version="1.0"?>
<Cars>
  <Car VIN="O002ABC002">
    <ProductionYear>2007</ProductionYear>
    <Model>Vectra</Model>
    <Features>
      <Feature Code="RXE">Reflektory xenonowe</Feature>
      <Feature Code="SZW">Sportowe zawieszenie</Feature>
      <Feature Code="SZD">Szyberdach</Feature>
    </Features>
  </Car>
  <Car VIN="O004ABC004">
    <ProductionYear>2005</ProductionYear>
    <Model>Astra</Model>
    <Features>
      <Feature Code="RXE">Reflektory xenonowe</Feature>
    </Features>
  </Car>
</Cars>
```

</p>
</details>


<details><summary>Cars.csv</summary>
<p>

```csv
VIN,Rok produkcji,Model
O002ABC002,2007,Vectra
O004ABC004,2005,Astra
```

</p>
</details>

## Unit Tests
Unit tests are implemented in xUnit 2.4.1.