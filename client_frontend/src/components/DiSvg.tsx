import React from "react";
import {Box} from "@material-ui/core";

interface Props {
  styles?: string;
}

const DiSvg = ({styles}: Props) => <Box className={styles}>
  <img src={'./dilogo.png'} alt={'DI logo'} />
</Box>

export default DiSvg;
