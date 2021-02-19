import React from 'react';
import {Button, Theme, withStyles} from "@material-ui/core";


const ColorButton = withStyles((theme: Theme) => ({
  root: {
    color: theme.palette.getContrastText('#5926EB'),
    backgroundColor: '#5926EB',
    width: '100%',
  },
}))(Button);

export default (props: any) => <ColorButton
  className={props.style}
  variant="contained"
  color="inherit"
>
  {props.children}
</ColorButton>