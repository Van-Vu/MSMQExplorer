<?xml version="1.0" encoding="iso-8859-1"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="/Request">
    <VistaLoyaltyMsg xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
      <xsl:comment>Vista Entertainment Solutions - All Rights Reserved</xsl:comment>
      <MsgID/>
      <MsgType>
        <xsl:value-of select="MsgType"/>
      </MsgType>
      <MsgTime>
        <xsl:value-of select="MsgTime"/>
      </MsgTime>
      <MsgResponseReq>
        <xsl:value-of select="MsgResponseReq"/>
      </MsgResponseReq>
      <DataItem DataItemType="MsgParamList">
        <xsl:for-each select="child::*">
          <xsl:if test="local-name(.)!='MsgType' and local-name(.)!='MsgTime' and local-name(.)!='MsgResponseReq' ">
            <Param>
              <xsl:attribute name="Name">
                <xsl:value-of select ="local-name(.)"/>
              </xsl:attribute>
              <xsl:value-of select="."/>
            </Param>
          </xsl:if>
        </xsl:for-each>
      </DataItem>
    </VistaLoyaltyMsg>
  </xsl:template>
</xsl:stylesheet>