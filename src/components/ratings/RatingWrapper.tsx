import React from 'react';
import {Grid} from "@material-ui/core";
import useStyles from "./useStyles";
import AdvancedRatingWrapper, {Props as AdvancedProps} from './AdvancedRatingWrapper';

export enum WrapperModes {
  basic = 'basic',
  advanced = 'advanced',
}
interface Props extends AdvancedProps {
  mode?: WrapperModes,
  classes?: string;
  sm?:  boolean | "auto" | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12;
  style?: any;
  title?: string;
}
const InitState = {
  mode: WrapperModes.basic,
}

const RatingWrapper = ({ children, classes, sm, title, style, mode, ...advancedProps }: Props = InitState) => {
  const styles = useStyles();
  return (
    <Grid item sm={sm} className={`${styles.paintContainer} ${classes}`} style={style}>
      <span className={`${styles.titleFont}`}>{title}</span>
      {
        mode === 'advanced' ?
          <AdvancedRatingWrapper {...advancedProps}>{children}</AdvancedRatingWrapper>
        :
          children
      }
    </Grid>
  );
}

export default RatingWrapper;