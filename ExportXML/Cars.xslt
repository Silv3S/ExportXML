<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <?xml-stylesheet href="Cars.xsl" type="text/xsl"?>
  <xsl:output method="text"/>

  
  
  <xsl:template match ="/">
    <xsl:text>VIN</xsl:text>,<xsl:text>Rok produkcji</xsl:text>,<xsl:text>Model</xsl:text>
    <xsl:text>&#xa;</xsl:text>
    <xsl:for-each select="Cars/Car">
            <xsl:value-of select="@VIN" />,<xsl:value-of select="ProductionYear" />,<xsl:value-of select="Model"/>
            <xsl:text>&#xa;</xsl:text>            
          </xsl:for-each>        
  </xsl:template>
</xsl:stylesheet>