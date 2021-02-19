import React from "react";
import {Box} from "@material-ui/core";

interface Props {
  styles?: string;
}

export default ({styles}: Props) => <Box className={styles}>
  <img src={'./dilogo.png'} />
</Box>