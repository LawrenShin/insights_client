import React from 'react';
import {Button, Theme, withStyles} from "@material-ui/core";


const ColorButton = withStyles((theme: Theme) => ({
  root: {
    color: theme.palette.getContrastText('#5926EB'),
    backgroundColor: '#5926EB',
    width: '100%',
  },
}))(Button);

export default ({onClick, style, children, ...rest}: any) => <ColorButton
  onClick={onClick}
  className={style}
  variant="contained"
  color="inherit"
  {...rest}
>
  {children}
</ColorButton>