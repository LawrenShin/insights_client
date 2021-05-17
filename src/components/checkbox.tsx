import React from 'react';
import {makeStyles, withStyles} from "@material-ui/core";
import Checkbox, { CheckboxProps } from '@material-ui/core/Checkbox';
import {paleGrey, purp} from "./colorConstants";

export const SimplePaintedChecbox = ({color, checkedColor}: { color: string, checkedColor: string }) => {
  const PaintedComponent = withStyles({
    root: {
      color: color,
      '&$checked': {
        color: checkedColor,
      },
    },
    checked: {},
  })((props: CheckboxProps) => <Checkbox color="default" {...props} />);

  return <PaintedComponent />
}

const useStyles = makeStyles({
  root: {
    '&:hover': {
      backgroundColor: 'transparent',
    },
  },
  icon: {
    borderRadius: 3,
    width: 16,
    height: 16,
    border: `1px solid ${paleGrey}`,

    backgroundImage: 'linear-gradient(180deg,hsla(0,0%,100%,.8),hsla(0,0%,100%,0))',
    '$root.Mui-focusVisible &': {
      outline: '2px auto rgba(19,124,189,.6)',
      outlineOffset: 2,
    },
    'input:hover ~ &': {
      backgroundColor: '#ebf1f5',
    },
    'input:disabled ~ &': {
      boxShadow: 'none',
      background: '#ccc',
    },
  },
  checkedIcon: {
    backgroundColor: '#F8F9FB',
    border: `1px solid ${purp}`,
    backgroundImage: 'linear-gradient(180deg,hsla(0,0%,100%,.1),hsla(0,0%,100%,0))',
    '&:before': {
      display: 'block',
      width: 16,
      height: 16,
      content: '""',
      backgroundImage: 'url(/checked.svg)',
      backgroundRepeat: 'no-repeat',
      backgroundPosition: 'center',
    },
    // 'input:hover ~ &': {
    //   backgroundColor: '#106ba3',
    // },
  },
});

export function StyledCheckbox(props: CheckboxProps) {
  const classes = useStyles();

  return (
    <Checkbox
      className={classes.root}
      disableRipple
      color="default"
      checkedIcon={<span className={`${classes.icon} ${classes.checkedIcon}`} />}
      icon={<span className={classes.icon} />}
      inputProps={{ 'aria-label': 'decorative checkbox' }}
      {...props}
    />
  );
}