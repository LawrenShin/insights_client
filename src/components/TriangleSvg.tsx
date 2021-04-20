import React from "react";
import {Box} from "@material-ui/core";

interface Props {
  onClick?: (e: React.SyntheticEvent) => void
  styles?: string;
  pale?: boolean
}

const TriangleSvg = ({pale, styles, onClick}: Props) => <Box className={styles} onClick={onClick}>

  <img src={pale ? '/trianglePale.svg' : '/triangle.svg'} alt={'Triangle'} />
</Box>

export default TriangleSvg;
