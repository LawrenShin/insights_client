import React from 'react';
import ArrowForwardIosIcon from "@material-ui/icons/ArrowForwardIos";
import ArrowBackIosIcon from '@material-ui/icons/ArrowBackIos';
import {makeStyles} from "@material-ui/core";
import {paleFont} from "./colorConstants";
import {ExpandMore} from "@material-ui/icons";


interface Props {
  // TODO: if needed more than once, finish implementation
  direction?: string;
  classes?: string;
  size?: string;
  onClick?: () => void,
}

const useStyles = makeStyles({
  ...paleFont,
})


const Arrow = ({ direction, classes, size, onClick }: Props) => {
  const styles = useStyles();
  const arrowProps = {
    className: `${classes} ${styles.paleFont}`,
    style: size ? { fontSize: size } : {},
    onClick,
  }

  if (!direction || direction === 'right') return <ArrowForwardIosIcon {...arrowProps} />
  if (direction === 'down') return <ExpandMore {...arrowProps} />
  if (direction === 'left') return <ArrowBackIosIcon {...arrowProps} />

  return null
}
export default Arrow;