
declare interface ObjectConstructor {
    assign<T, U>(target: T, source: U): T & U;
    assign<T, U, V>(target: T, source1: U, source2: V): T & U & V;
    assign<T, U, V, W>(target: T, source1: U, source2: V, source3: W): T & U & V & W;
    assign(target: any, ...sources: any[]): any;
}

if (typeof Object.assign != 'function') {
    Object.assign = function<T1, T2>(target: T1, varArgs: T2[]) { // .length of function is 2
        'use strict';
        if (target == null) { // TypeError if undefined or null
            throw new TypeError('Cannot convert undefined or null to object');
        }

        var to = Object(target);

        for (var index = 1; index < arguments.length; index++) {
            var nextSource = arguments[index];

            if (nextSource != null) { // Skip over if undefined or null
                for (var nextKey in nextSource) {
                    // Avoid bugs when hasOwnProperty is shadowed
                    if (Object.prototype.hasOwnProperty.call(nextSource, nextKey)) {
                        to[nextKey] = nextSource[nextKey];
                    }
                }
            }
        }
        return to;
    };
}

declare interface Array<T> {
    find(callback: (element: T) => boolean): T | undefined;
    findIndex(callback: (element: T) => boolean): number;

    /**
     * Performs swaps only if elements compare as non-equal.
     * Differs from Array.prototype.sort, which can swap elements that compare as equal.
     */
    stableSort(cmp: (lhs: T, rhs: T) => number): T[];
}

// https://tc39.github.io/ecma262/#sec-array.prototype.find
if (!Array.prototype.find) {
    Object.defineProperty(Array.prototype, 'find', {
        value: function (predicate: (element: any) => void) {
            // 1. Let O be ? ToObject(this value).
            if (this == null) {
                throw new TypeError('"this" is null or not defined');
            }

            var o = Object(this);

            // 2. Let len be ? ToLength(? Get(O, "length")).
            var len = o.length >>> 0;

            // 3. If IsCallable(predicate) is false, throw a TypeError exception.
            if (typeof predicate !== 'function') {
                throw new TypeError('predicate must be a function');
            }

            // 4. If thisArg was supplied, let T be thisArg; else let T be undefined.
            var thisArg = arguments[1];

            // 5. Let k be 0.
            var k = 0;

            // 6. Repeat, while k < len
            while (k < len) {
                // a. Let Pk be ! ToString(k).
                // b. Let kValue be ? Get(O, Pk).
                // c. Let testResult be ToBoolean(? Call(predicate, T, « kValue, k, O »)).
                // d. If testResult is true, return kValue.
                var kValue = o[k];
                if (predicate.call(thisArg, kValue, k, o)) {
                    return kValue;
                }
                // e. Increase k by 1.
                k++;
            }

            // 7. Return undefined.
            return undefined;
        }
    });
}

// https://tc39.github.io/ecma262/#sec-array.prototype.findIndex
if (!Array.prototype.findIndex) {
    Object.defineProperty(Array.prototype, 'findIndex', {
        value: function (predicate: (element: any) => boolean) {
            // 1. Let O be ? ToObject(this value).
            if (this == null) {
                throw new TypeError('"this" is null or not defined');
            }

            var o = Object(this);

            // 2. Let len be ? ToLength(? Get(O, "length")).
            var len = o.length >>> 0;

            // 3. If IsCallable(predicate) is false, throw a TypeError exception.
            if (typeof predicate !== 'function') {
                throw new TypeError('predicate must be a function');
            }

            // 4. If thisArg was supplied, let T be thisArg; else let T be undefined.
            var thisArg = arguments[1];

            // 5. Let k be 0.
            var k = 0;

            // 6. Repeat, while k < len
            while (k < len) {
                // a. Let Pk be ! ToString(k).
                // b. Let kValue be ? Get(O, Pk).
                // c. Let testResult be ToBoolean(? Call(predicate, T, « kValue, k, O »)).
                // d. If testResult is true, return k.
                var kValue = o[k];
                if (predicate.call(thisArg, kValue, k, o)) {
                    return k;
                }
                // e. Increase k by 1.
                k++;
            }

            // 7. Return -1.
            return -1;
        }
    });
}

if (!Array.prototype.stableSort) {

    // We need a stable sort, so here is someone's mergesort implementation:
    // http://stackoverflow.com/a/1427621/2855742
    function merge<T>(left: T[], right: T[], cmp: (lhs: T, rhs: T) => number): T[] {
        let result: T[] = [];

        while (left.length && right.length) {
            if (cmp(left[0], right[0]) <= 0) {
                result.push(left.shift()!);
            } else {
                result.push(right.shift()!);
            }
        }

        while (left.length)
            result.push(left.shift()!);

        while (right.length)
            result.push(right.shift()!);

        return result;
    }

    function mergeSort<T>(this: T[], cmp: (lhs: T, rhs: T) => number): T[] {
        if (this.length < 2)
            return this;

        var middle = Math.floor(this.length / 2);
        var left = this.slice(0, middle);
        var right = this.slice(middle, this.length);

        return merge(left.stableSort(cmp), right.stableSort(cmp), cmp);
    }

    Array.prototype.stableSort = mergeSort;
}
