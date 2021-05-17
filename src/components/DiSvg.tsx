import React from "react";
import {Box} from "@material-ui/core";

interface Props {
  onClick?: (e: React.SyntheticEvent) => void
  styles?: string;
}

const DiSvg = ({styles, onClick}: Props) => <Box className={styles} onClick={onClick}>
  <img src={'/dinominator_logo.png'} alt={'DI logo'} />
</Box>

export default DiSvg;
