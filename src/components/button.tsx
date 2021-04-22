import React from 'react';
import {Button, Theme, withStyles} from "@material-ui/core";
import {purp} from "./colorConstants";


const ColorButtonSquared = withStyles((theme: Theme) => ({
  root: {
    color: theme.palette.getContrastText('#5926EB'),
    backgroundColor: '#5926EB',
    width: '100%',
  },
}))(Button);

const ColorButtonRounded = withStyles((theme: Theme) => ({
  root: {
    fontFamily: 'open sans',
    fontWeight: 400,
    color: purp,
    background: '#fff',
    border: `1px solid ${purp}`,
    borderRadius: '50px',
    margin: '0px 15px',
    height: '35px',
    padding: '0 25px',
    '&:hover': {
      cursor: 'pointer',
      color: '#fff',
      background: purp
    },
  },
}))(Button);

export const Squared = ({onClick, styles, children, ...rest}: any) => <ColorButtonSquared
  onClick={onClick}
  className={styles}
  variant="contained"
  color="inherit"
  {...rest}
>
  {children}
</ColorButtonSquared>

export const Rounded = ({onClick, style, children, ...rest}: any) => <ColorButtonRounded
  onClick={onClick}
  className={style}
  variant="contained"
  color="inherit"
  {...rest}
>
  {children}
</ColorButtonRounded>