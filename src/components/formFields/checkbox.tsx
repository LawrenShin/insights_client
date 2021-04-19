import React from "react";
import { useField } from 'formik';
import {Checkbox, FormControl, FormControlLabel} from '@material-ui/core';

const MyCheckbox = (props: any) => {
  const { errorText, size, required, label, className, ...rest } = props;
  const [field, meta] = useField(props);

  function _renderHelperText() {
    const {touched, error} = meta;
    if (touched && error) {
      return error;
    }
  }

  return (
    <FormControlLabel
      className={className}
      control={
        <Checkbox
          {...field}
          style={{color: '#5926EB'}}
          required={required}
        />
      }
      label={label}
    />
  );
}

export default MyCheckbox;